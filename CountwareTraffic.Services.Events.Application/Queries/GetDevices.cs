using Convey.CQRS.Queries;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Events.Application
{
    public class GetDevices : IQuery<IList<DeviceDto>>
    {
    }
}
