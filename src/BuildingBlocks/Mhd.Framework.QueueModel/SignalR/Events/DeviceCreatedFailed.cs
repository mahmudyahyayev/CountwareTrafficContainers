using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record DeviceCreatedFailed : IQueueEvent
    {
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string DeviceCreationStatus { get; set; }
        public Guid RecordId { get; init; }
    }
}
