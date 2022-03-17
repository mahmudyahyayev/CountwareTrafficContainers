using System;

namespace CountwareTraffic.Services.Devices.Application
{
    public class DeviceChangedRejected : Convey.CQRS.Events.IEvent
    {
        public Guid DeviceId { get; set; }
        public string Name { get; set; }
        public string OldName { get; set; }
        public Guid UserId { get; set; }
    }
}
