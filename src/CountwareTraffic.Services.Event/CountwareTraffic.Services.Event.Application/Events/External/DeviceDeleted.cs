using System;

namespace CountwareTraffic.Services.Events.Application
{
    public class DeviceDeleted : Convey.CQRS.Events.IEvent
    {
        public Guid DeviceId { get; init; }
        public Guid UserId { get; init; }
        public string Name { get; set; }
    }
}
