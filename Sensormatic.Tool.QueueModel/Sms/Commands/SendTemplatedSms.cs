using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
{
    public record SendTemplatedSms : IQueueCommand
    {
        public string SmsTemplateAssemblyName { get; set; }
        public dynamic SmsTemplate { get; set; }
        public Guid RecordId { get ; init ; }
    }
}
