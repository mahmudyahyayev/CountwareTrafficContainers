using Convey.CQRS.Queries;
using CountwareTraffic.Services.Devices.Application;
using CountwareTraffic.Services.Devices.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class GetDeviceTypesHandler : IQueryHandler<GetDeviceTypes, IEnumerable<DeviceTypeDto>>
    {
        public async Task<IEnumerable<DeviceTypeDto>> HandleAsync(GetDeviceTypes query)
        {
            return DeviceType.List().Select(type => new DeviceTypeDto { Id = type.Id, Name = type.Name });
        }
    }
}
