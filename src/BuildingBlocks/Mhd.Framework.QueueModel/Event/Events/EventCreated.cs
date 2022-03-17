using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record EventCreated : MessageEnvelope, IQueueEvent
    {
        public Guid DeviceId { get; init; }
        public Guid EventId { get; init; }
        public string DeviceName { get; init; }
        public int DirectionTypeId { get; set; }
        public string DirectionTypeName { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public Guid RecordId { get; init; }
        public DateTime CreateDate { get; set; }
        public Guid CreateBy { get; init; }
    }
}
