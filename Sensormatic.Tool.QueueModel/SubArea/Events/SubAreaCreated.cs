using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
{
    public record SubAreaCreated : MessageEnvelope, IQueueEvent
    {
        public Guid SubAreaId { get; init; }
        public string Name { get; init; }
        public Guid RecordId { get; init; }
    }
}
