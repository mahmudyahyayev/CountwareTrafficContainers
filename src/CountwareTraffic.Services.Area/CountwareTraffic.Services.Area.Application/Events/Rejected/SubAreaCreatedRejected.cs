using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public class SubAreaCreatedRejected : Convey.CQRS.Events.IEvent
    {
        public Guid SubAreaId { get; init; }
        public Guid UserId { get; set; }
    }
}
