using System;

namespace CountwareTraffic.Services.Events.Application
{
    public class EventDetailsDto : EventDto
    {
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public Guid DeviceId { get; set; }
        public int DirectionTypeId { get; set; }
        public string DirectionTypeName { get; set; }
        public string DeviceName { get; set; }
    }
}
