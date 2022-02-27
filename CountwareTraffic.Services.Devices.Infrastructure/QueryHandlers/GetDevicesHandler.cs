using Convey.CQRS.Queries;
using CountwareTraffic.Services.Devices.Application;
using CountwareTraffic.Services.Devices.Core;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class GetDevicesHandler : IQueryHandler<GetDevices, PagingResult<DeviceDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDevicesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagingResult<DeviceDetailsDto>> HandleAsync(GetDevices query)
        {
            int page = query.PagingQuery.Page;
            int limit = query.PagingQuery.Limit;

            var qres = await _unitOfWork
                                        .GetRepository<IDeviceRepository>()
                                        .GetAllAsync(page, limit, query.SubAreaId);

            if (qres == null)
                return PagingResult<DeviceDetailsDto>.Empty;

            bool hasNextPage = qres.Total > (limit * (page - 1)) + qres.Entities.Count;

            return new PagingResult<DeviceDetailsDto>(qres.Entities.Select(device => new DeviceDetailsDto
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
            }), qres.Total, page, limit, hasNextPage);
        }
    }
}
