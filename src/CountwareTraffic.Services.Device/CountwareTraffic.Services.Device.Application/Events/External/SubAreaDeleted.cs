using System;

namespace CountwareTraffic.Services.Devices.Application
{
    public class SubAreaDeleted : Convey.CQRS.Events.IEvent
    {
        public Guid SubAreaId { get; init; }
        public Guid UserId { get; set; }
    }
}
