using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Application
{
    public class DeviceStatusChangedHandler : IEventHandler<DeviceStatusChanged>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeviceStatusChangedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeviceStatusChanged command)
        {
            var deviceRepository = _unitOfWork.GetRepository<IDeviceRepository>();

            var device = await deviceRepository.GetAsync(command.DeviceId);

            device.WhenChangeStatus(command.DeviceStatusId);

            await _unitOfWork.CommitAsync();
        }
    }
}
