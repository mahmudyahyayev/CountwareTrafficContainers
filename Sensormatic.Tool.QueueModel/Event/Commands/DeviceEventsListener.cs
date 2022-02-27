using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
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
