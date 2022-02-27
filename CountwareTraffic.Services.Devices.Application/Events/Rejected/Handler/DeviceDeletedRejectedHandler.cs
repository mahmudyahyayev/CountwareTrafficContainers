using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Core;
using Sensormatic.Tool.Queue;
using Sensormatic.Tool.QueueModel;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Application
{
    public class DeviceDeletedRejectedHandler : IEventHandler<DeviceDeletedRejected>
    {
        private readonly IQueueService _queueService;
        private readonly IUnitOfWork _unitOfWork;
        public DeviceDeletedRejectedHandler(IQueueService queueService, IUnitOfWork unitOfWork)
        {
            _queueService = queueService;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeviceDeletedRejected command)
        {
            var deviceRepository = _unitOfWork.GetRepository<IDeviceRepository>();

            var device = await deviceRepository.GetDeletedAsync(command.DeviceId);

            device.WhenDeletedRejected();

            await _unitOfWork.CommitAsync();

            //SignalR ile frontendi besleme kismi
            _queueService.Publish(new DeviceDeletedFailed
            {
                RecordId = Guid.NewGuid(),
                DeviceId = command.DeviceId,
                UserId = command.UserId,
                UserName = "Test" //todo
            });
        }
    }
}
