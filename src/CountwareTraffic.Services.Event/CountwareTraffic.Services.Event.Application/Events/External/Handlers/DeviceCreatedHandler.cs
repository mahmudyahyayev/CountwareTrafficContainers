using Convey.CQRS.Events;
using CountwareTraffic.Services.Events.Core;
using Mhd.Framework.Queue;
using Mhd.Framework.QueueModel;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Application
{
    public class DeviceCreatedHandler : IEventHandler<DeviceCreated>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueService _queueService;
        public DeviceCreatedHandler(IUnitOfWork unitOfWork, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _queueService = queueService;
        }

        public async Task HandleAsync(DeviceCreated @event)
        {
            var deviceRepository = _unitOfWork.GetRepository<IDeviceRepository>();

            if (!await deviceRepository.ExistsAsync(@event.DeviceId))
            {
                try
                { 
                    var device = Device.Create(@event.DeviceId, @event.Name);

                    await deviceRepository.AddAsync(device);

                    await _unitOfWork.CommitAsync();

                    _queueService.Publish(new DeviceCreatedCompleted { DeviceId = @event.DeviceId , Name = @event.Name});
                }
                catch (Exception ex)
                {
                    _queueService.Publish(new DeviceCreatedRejected { DeviceId = @event.DeviceId, Name = @event.Name });
                }
            }
        }
    }
}
