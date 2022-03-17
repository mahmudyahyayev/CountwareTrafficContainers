using Convey.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Application
{
    public class GetDeviceStatuses : IQuery<IEnumerable<DeviceStatusDto>>
    {
    }
}
