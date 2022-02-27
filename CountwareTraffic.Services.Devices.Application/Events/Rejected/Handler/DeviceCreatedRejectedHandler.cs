using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Core;
using Sensormatic.Tool.Queue;
using Sensormatic.Tool.QueueModel;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Application
{
    public class DeviceCreatedRejectedHandler : IEventHandler<DeviceCreatedRejected>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueService _queueService;
        public DeviceCreatedRejectedHandler(IUnitOfWork unitOfWork, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _queueService = queueService;
        }

        public async Task HandleAsync(DeviceCreatedRejected command)
        {
            var deviceRepository = _unitOfWork.GetRepository<IDeviceRepository>();

            var device = await deviceRepository.GetAsync(command.DeviceId);

            device.WhenCreatedRejected();

            await _unitOfWork.CommitAsync();

            //SignalR ile frontendi besleme kismi
            _queueService.Publish(new DeviceCreatedFailed
            {
                RecordId = Guid.NewGuid(),
                DeviceId = command.DeviceId,
                UserId = command.UserId,
                DeviceCreationStatus = DeviceCreationStatus.Rejected.Name,
                UserName = "Test" //todo
            });
        }
    }
}
