using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class SubAreaChangedRejected : Convey.CQRS.Events.IEvent
    {
        public Guid SubAreaId { get; init; }
        public string Name { get; set; }
        public string OldName { get; set; }
        public Guid UserId { get; set; }
    }
}
