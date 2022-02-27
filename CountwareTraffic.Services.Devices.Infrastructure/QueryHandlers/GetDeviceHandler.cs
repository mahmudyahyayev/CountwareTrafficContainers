using Convey.CQRS.Queries;
using CountwareTraffic.Services.Devices.Application;
using CountwareTraffic.Services.Devices.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class GetDeviceHandler : IQueryHandler<GetDevice, DeviceDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDeviceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeviceDetailsDto> HandleAsync(GetDevice query)
        {
            var device = await _unitOfWork.GetRepository<IDeviceRepository>()
                .GetAsync(query.DeviceId);

            if (device == null)
                throw new DeviceNotFoundException(query.DeviceId);

            return new DeviceDetailsDto
            {
                Id = device.Id,
                Name = device.Name,
                Description = device.Description,
                AuditCreateBy = device.AuditCreateBy,
                AuditCreateDate = device.AuditCreateDate,
                AuditModifiedBy = device.AuditModifiedBy,
                AuditModifiedDate = device.AuditModifiedDate,
                DeviceTypeName = device.DeviceType.Name,
                DeviceStatusName = device.DeviceStatus.Name,
                SubAreaId = device.SubAreaId,
                DeviceStatusId = device.DeviceStatus.Id,
                DeviceTypeId = device.DeviceType.Id,
                Identity = device.ConnectionInfo.Identity,
                IpAddress = device.ConnectionInfo.IpAddress,
                MacAddress = device.ConnectionInfo.MacAddress,
                Model = device.Model,
                Password = device.ConnectionInfo.Password,
                Port = device.ConnectionInfo.Port,
                UniqueId = device.ConnectionInfo.UniqueId
            };
        }
    }
}
