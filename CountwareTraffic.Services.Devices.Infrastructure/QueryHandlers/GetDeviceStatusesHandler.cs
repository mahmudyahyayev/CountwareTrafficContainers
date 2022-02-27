using Convey.CQRS.Queries;
using CountwareTraffic.Services.Devices.Application;
using CountwareTraffic.Services.Devices.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class GetDeviceStatusesHandler : IQueryHandler<GetDeviceStatuses, IEnumerable<DeviceStatusDto>>
    {
        public async Task<IEnumerable<DeviceStatusDto>> HandleAsync(GetDeviceStatuses query)
        {
            return DeviceStatus.List().Select(type => new DeviceStatusDto { Id = type.Id, Name = type.Name });
        }
    }
}
