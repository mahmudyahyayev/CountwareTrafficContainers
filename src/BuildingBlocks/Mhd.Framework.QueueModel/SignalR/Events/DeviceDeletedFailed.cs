using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record DeviceDeletedFailed : IQueueEvent
    {
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsDeleted { get; } = false;
        public Guid RecordId { get; init; }
    }
}
