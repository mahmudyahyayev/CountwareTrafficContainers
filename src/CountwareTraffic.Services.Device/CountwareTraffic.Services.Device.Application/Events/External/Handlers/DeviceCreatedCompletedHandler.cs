using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Core;
using Mhd.Framework.Queue;
using Mhd.Framework.QueueModel;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Application
{
    public class DeviceCreatedCompletedHandler : IEventHandler<DeviceCreatedCompleted>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueService _queueService;
        public DeviceCreatedCompletedHandler(IUnitOfWork unitOfWork, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _queueService = queueService;
        }

        public async Task HandleAsync(DeviceCreatedCompleted command)
        {
            var deviceRepository = _unitOfWork.GetRepository<IDeviceRepository>();

            var device = await deviceRepository.GetAsync(command.DeviceId);

            device.WhenCreatedCompleted();

            await _unitOfWork.CommitAsync();

            //SignalR ile frontendi besleme kismi
            _queueService.Publish(new DeviceCreatedSuccessfully
            {
                RecordId = Guid.NewGuid(),
                DeviceId = command.DeviceId,
                DeviceCreationStatus = DeviceCreationStatus.Completed.Name,
                UserId = command.UserId,
                UserName = "test",
                DeviceName = command.Name
            });
        }
    }
}
