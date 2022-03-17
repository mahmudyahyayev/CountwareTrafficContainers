using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record SubAreaCreated : MessageEnvelope, IQueueEvent
    {
        public Guid SubAreaId { get; init; }
        public string Name { get; init; }
        public Guid RecordId { get; init; }
    }
}
