using Convey.CQRS.Queries;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Application
{
    public class GetDeviceTypes : IQuery<IEnumerable<DeviceTypeDto>>
    {
    }
}
