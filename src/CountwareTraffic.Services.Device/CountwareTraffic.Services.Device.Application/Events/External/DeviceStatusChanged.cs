using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Application
{
    public class DeviceStatusChanged : Convey.CQRS.Events.IEvent
    {
        public Guid DeviceId { get; init; }
        public int DeviceStatusId { get; set; }
        public Guid RecordId { get; init; }
    }
}
