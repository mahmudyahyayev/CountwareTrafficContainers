using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{ 
    public record DeviceEventsListener : MessageEnvelope, IQueueCommand
    {
        public string Description { get; init; }
        public Guid DeviceId { get; init; }
        public int DirectionTypeId { get; init; }
        public Guid RecordId { get; init; }
        public DateTime EventDate { get; set; }
    }
}
