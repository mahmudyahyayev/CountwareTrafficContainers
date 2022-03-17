using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record SubAreaCreatedCompleted : MessageEnvelope, IQueueEvent
    {
        public Guid SubAreaId { get; init; }
        public Guid RecordId { get; init; }
    }
}
