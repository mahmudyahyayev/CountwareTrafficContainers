using System;

namespace CountwareTraffic.Services.Events.Application
{
    public class DeviceEventsListener : Convey.CQRS.Commands.ICommand
    {
        public string Description { get; init; }
        public Guid DeviceId { get; init; }
        public int DirectionTypeId { get; init; }
        public Guid RecordId { get; init; }
        public DateTime EventDate { get; set; }
        public Guid UserId { get; set; }
        public string DeviceName { get; set; }
    }
}
