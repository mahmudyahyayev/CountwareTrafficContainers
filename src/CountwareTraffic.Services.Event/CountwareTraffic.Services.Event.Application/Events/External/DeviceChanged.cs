using System;

namespace CountwareTraffic.Services.Events.Application
{
    public class DeviceChanged : Convey.CQRS.Events.IEvent
    {
        public Guid DeviceId { get; init; }
        public string Name { get; init; }
        public string OldName { get; set; }
        public Guid UserId { get; init; }
    }
}
