using Convey.CQRS.Commands;
using CountwareTraffic.Services.Devices.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Application
{
    public class UpdateDeviceHandler : ICommandHandler<UpdateDevice>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateDeviceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(UpdateDevice command)
        {
            var deviceRepository = _unitOfWork.GetRepository<IDeviceRepository>();

            var subArea = await deviceRepository.GetAsync(command.DeviceId);

            if (subArea is null)
                throw new DeviceNotFoundException(command.DeviceId);

            if (subArea.Name == command.Name)
            {
                if (subArea.Description == command.Description)
                    return;

                subArea.WhenChanged(command.Name, command.Description);

                await _unitOfWork.CommitAsync();
                return;
            }


            if (await deviceRepository.ExistsAsync(command.Name))
                throw new DeviceAlreadyExistsException(command.Name);

            subArea.WhenChanged(command.Name, command.Description);

            await _unitOfWork.CommitAsync();
        }
    }
}
