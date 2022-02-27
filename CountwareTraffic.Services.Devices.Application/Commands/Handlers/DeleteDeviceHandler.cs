using Convey.CQRS.Commands;
using CountwareTraffic.Services.Devices.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Application
{
    public class DeleteDeviceHandler : ICommandHandler<DeleteDevice>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteDeviceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeleteDevice command)
        {
            var deviceRepository = _unitOfWork.GetRepository<IDeviceRepository>();

            var device = await deviceRepository.GetAsync(command.DeviceId);

            if (device is null) throw new DeviceNotFoundException(command.DeviceId);

            deviceRepository.Remove(device);

            device.WhenDeleted(command.DeviceId);

            await _unitOfWork.CommitAsync();
        }
    }
}
