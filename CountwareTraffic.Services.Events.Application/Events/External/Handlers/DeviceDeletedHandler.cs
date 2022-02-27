using Convey.CQRS.Events;
using CountwareTraffic.Services.Events.Core;
using Sensormatic.Tool.Queue;
using Sensormatic.Tool.QueueModel;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Application
{
    public class DeviceDeletedHandler : IEventHandler<DeviceDeleted>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueService _queueService;
        public DeviceDeletedHandler(IUnitOfWork unitOfWork, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _queueService = queueService;
        }

        public async Task HandleAsync(DeviceDeleted @event)
        {
            var deviceRepository = _unitOfWork.GetRepository<IDeviceRepository>();

            var device = await deviceRepository.GetAsync(@event.DeviceId);

            if (device != null)
            {
                try
                {
                    deviceRepository.Remove(device);
                    await _unitOfWork.CommitAsync();

                    //SignalR ile frontendi besleme kismi
                    _queueService.Publish(new DeviceDeletedSuccessfully
                    {
                        RecordId = Guid.NewGuid(),
                        DeviceName = @event.Name,
                        DeviceId = @event.DeviceId,
                        UserId = @event.UserId,
                        UserName = "Test" //todo
                    });
                }
                catch { _queueService.Publish(new DeviceDeletedRejected { DeviceId = @event.DeviceId }); }
            }
        }
    }
}