using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
{
    public record SendTemplatedEmail : IQueueCommand
    {
        public string EmailTemplateAssemblyName { get; set; }
        public dynamic EmailTemplate { get; set; }
        public Guid RecordId { get; init; }
    }
}
