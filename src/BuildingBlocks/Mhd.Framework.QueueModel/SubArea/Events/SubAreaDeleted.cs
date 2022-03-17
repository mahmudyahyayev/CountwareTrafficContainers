using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record SubAreaDeleted : MessageEnvelope, IQueueEvent
    {
        public Guid SubAreaId { get; init; }
        public Guid RecordId { get; init; }
    }
}
