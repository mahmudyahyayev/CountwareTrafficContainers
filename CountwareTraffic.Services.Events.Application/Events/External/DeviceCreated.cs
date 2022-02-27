using System;

namespace CountwareTraffic.Services.Events.Application
{
    public class DeviceCreated : Convey.CQRS.Events.IEvent
    {
        public Guid DeviceId { get; init; }
        public string Name { get; init; }
        public Guid UserId { get; init; }
    }
}
