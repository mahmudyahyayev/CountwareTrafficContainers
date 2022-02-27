using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
{
    public record SubAreaChangedRejected : MessageEnvelope, IQueueEvent
    {
        public Guid SubAreaId { get; init; }
        public string Name { get; init; }
        public string OldName { get; init; }
        public Guid RecordId { get; init; }
    }
}
