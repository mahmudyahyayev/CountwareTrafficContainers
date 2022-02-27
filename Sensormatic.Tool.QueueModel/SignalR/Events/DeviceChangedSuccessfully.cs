using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
{
    public record DeviceChangedSuccessfully : IQueueEvent
    {
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public Guid RecordId { get; init; }
        public string UserName { get; set; }
    }
}
