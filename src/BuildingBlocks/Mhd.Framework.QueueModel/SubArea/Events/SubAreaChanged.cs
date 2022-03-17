using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record SubAreaChanged : MessageEnvelope, IQueueEvent
    {
        public Guid SubAreaId { get; init; }
        public string Name { get; init; }
        public string OldName { get; init; }
        public Guid RecordId { get; init; }
    }
}
