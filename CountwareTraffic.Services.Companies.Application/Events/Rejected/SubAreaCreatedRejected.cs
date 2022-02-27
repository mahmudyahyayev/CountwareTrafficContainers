using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class SubAreaCreatedRejected : Convey.CQRS.Events.IEvent
    {
        public Guid SubAreaId { get; init; }
        public Guid UserId { get; set; }
    }
}
