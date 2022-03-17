using Convey.CQRS.Commands;
using CountwareTraffic.Services.Devices.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Application
{
    public class CreateDeviceHandler : ICommandHandler<CreateDevice>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateDeviceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(CreateDevice command)
        {
            var deviceRepository = _unitOfWork.GetRepository<IDeviceRepository>();

            if (await deviceRepository.ExistsAsync(command.Name))
                throw new DeviceAlreadyExistsException(command.Name);

            var subArea = await _unitOfWork.GetRepository<ISubAreaRepository>().GetAsync(command.SubAreaId);

            if (subArea == null)
                command.SubAreaId = System.Guid.Empty;

            var connectionInfo = DeviceConnectionInfo.Create(command.IpAddress, command.Port, command.Identity, command.Password, command.UniqueId, command.MacAddress);

            var device = Device.Create(command.Name, command.Description, command.Model, command.SubAreaId, connectionInfo,  command.DeviceTypeId);

            await deviceRepository.AddAsync(device);

            device.WhenCreated(device.Id, device.Name);

            await _unitOfWork.CommitAsync();
        }
    }
}
