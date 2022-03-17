using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record DeviceDeleted : MessageEnvelope, IQueueEvent
    {
        public Guid DeviceId { get; init; }
        public Guid RecordId { get; init; }
        public string Name { get; set; }
    }
}
