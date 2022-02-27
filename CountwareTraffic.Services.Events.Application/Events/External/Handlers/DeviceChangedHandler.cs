using Convey.CQRS.Events;
using CountwareTraffic.Services.Events.Core;
using Sensormatic.Tool.Queue;
using Sensormatic.Tool.QueueModel;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Application
{
    public class DeviceChangedHandler : IEventHandler<DeviceChanged>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueService _queueService;
        public DeviceChangedHandler(IUnitOfWork unitOfWork, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _queueService = queueService;
        }

        public async Task HandleAsync(DeviceChanged @event)
        {
            var device = await _unitOfWork.GetRepository<IDeviceRepository>()
                 .GetAsync(@event.DeviceId);

            if (device != null)
            {
                try
                {
                    device.Change(@event.Name);

                    await _unitOfWork.CommitAsync();

                    //SignalR ile frontendi besleme kismi
                    _queueService.Publish(new DeviceChangedSuccessfully
                    {
                        RecordId = Guid.NewGuid(),
                        NewName = @event.Name,
                        OldName = @event.OldName,
                        DeviceId = @event.DeviceId,
                        UserId = @event.UserId,
                        UserName = "Test" //todo
                    });
                }
                catch (Exception ex)
                {
                    _queueService.Publish(new DeviceChangedRejected { DeviceId = @event.DeviceId, Name = @event.Name, OldName = @event.OldName });
                }
            }
        }
    }
}
