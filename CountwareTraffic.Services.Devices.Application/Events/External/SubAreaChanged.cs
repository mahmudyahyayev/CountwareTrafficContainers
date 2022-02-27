using System;

namespace CountwareTraffic.Services.Devices.Application
{
    public class SubAreaChanged : Convey.CQRS.Events.IEvent
    {
        public Guid SubAreaId { get; init; }
        public string Name { get; init; }
        public string OldName { get; set; }
        public Guid UserId { get; set; }
    }
}
