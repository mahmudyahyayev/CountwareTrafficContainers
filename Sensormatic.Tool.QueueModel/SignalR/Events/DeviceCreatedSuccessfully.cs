using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
{
    public record DeviceCreatedSuccessfully : IQueueEvent
    {
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string DeviceCreationStatus { get; set; }
        public Guid RecordId { get; init; }
        public string DeviceName { get; set; }
    }
}
