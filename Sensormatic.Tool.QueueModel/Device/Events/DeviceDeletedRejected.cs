using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
{
    public record DeviceDeletedRejected : MessageEnvelope, IQueueEvent
    {
        public Guid DeviceId { get; init; }
        public Guid RecordId { get; init; }
    }
}
