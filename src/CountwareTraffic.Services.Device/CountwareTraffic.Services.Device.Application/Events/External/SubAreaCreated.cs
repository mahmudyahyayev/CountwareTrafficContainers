using Convey.CQRS.Events;
using System;

namespace CountwareTraffic.Services.Devices.Application
{
    public class SubAreaCreated : Convey.CQRS.Events.IEvent
    {
        public Guid SubAreaId { get; init; }
        public string Name { get; init; }
        public Guid UserId { get; set; }
    }
}
