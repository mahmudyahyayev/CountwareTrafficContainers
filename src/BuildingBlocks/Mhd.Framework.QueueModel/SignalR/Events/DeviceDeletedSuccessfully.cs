using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record DeviceDeletedSuccessfully : IQueueEvent
    {
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsDeleted { get; } = true;
        public Guid RecordId { get; init; }
    }
}
