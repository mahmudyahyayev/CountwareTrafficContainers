using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
{
    public record SubAreaCreatedRejected : MessageEnvelope, IQueueEvent
    {
        public Guid SubAreaId { get; init; }
        public Guid RecordId { get; init; }
    }
}
