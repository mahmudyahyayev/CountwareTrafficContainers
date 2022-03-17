using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record DeviceStatusChanged :IQueueEvent
    {
        public Guid DeviceId { get; init; }
        public int DeviceStatusId { get; set; }
        public Guid RecordId { get; init; }
    }
}
