using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public class SubAreaDeletedRejected : Convey.CQRS.Events.IEvent
    {
        public Guid SubAreaId { get; init; }
        public Guid UserId { get; set; }
    }
}
