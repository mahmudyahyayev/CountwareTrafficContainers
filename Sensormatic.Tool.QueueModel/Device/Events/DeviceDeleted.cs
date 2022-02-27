using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
{
    public record DeviceDeleted : MessageEnvelope, IQueueEvent
    {
        public Guid DeviceId { get; init; }
        public Guid RecordId { get; init; }
        public string Name { get; set; }
    }
}
