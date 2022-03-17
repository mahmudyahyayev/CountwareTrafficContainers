using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record SendTemplatedSms : IQueueCommand
    {
        public string SmsTemplateAssemblyName { get; set; }
        public dynamic SmsTemplate { get; set; }
        public Guid RecordId { get ; init ; }
    }
}
