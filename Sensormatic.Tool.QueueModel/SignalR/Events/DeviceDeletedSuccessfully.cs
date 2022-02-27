using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
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
