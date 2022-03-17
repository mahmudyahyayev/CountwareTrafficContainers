using Mhd.Framework.Efcore;
using System;

namespace CountwareTraffic.Services.Events.Core
{
    public class EventCreateCompleted : IDomainEvent
    {
        public Guid DeviceId { get; init; }
        public Guid EventId { get; init; }
        public string DeviceName { get; init; }
        public int DirectionTypeId { get; set; }
        public string DirectionTypeName { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime  CreateDate { get; set; }
        public Guid CreateBy { get; set; }
        public Guid RecordId { get; init; }
        public EventCreateCompleted(Guid deviceId, Guid eventId, string deviceName, DirectionType directionType, string description, DateTime eventDate, DateTime createDate, Guid createBy)
        {
            DeviceId = deviceId;
            EventId = eventId;
            DeviceName = deviceName;
            DirectionTypeId = directionType.Id;
            DirectionTypeName = directionType.Name;
            Description = description;
            EventDate = eventDate;
            CreateDate = createDate;
            CreateBy = createBy;
            RecordId = Guid.NewGuid();
        }
    }
}
