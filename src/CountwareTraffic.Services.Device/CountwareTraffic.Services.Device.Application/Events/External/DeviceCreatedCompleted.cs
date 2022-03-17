using System;

namespace CountwareTraffic.Services.Devices.Application
{
    public class DeviceCreatedCompleted : Convey.CQRS.Events.IEvent
    {
        public Guid DeviceId { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}
