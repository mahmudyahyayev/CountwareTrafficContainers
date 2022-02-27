using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
{
    public record DeviceChangedRejected : MessageEnvelope, IQueueEvent
    {
        public Guid DeviceId { get; init; }
        public string Name { get; init; }
        public string OldName { get; init; }
        public Guid RecordId { get; init; }
    }
}
