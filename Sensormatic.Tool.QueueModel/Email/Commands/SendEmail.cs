using Sensormatic.Tool.Queue;
using System;
using System.Collections.Generic;

namespace Sensormatic.Tool.QueueModel
{
    public record SendEmail : IQueueCommand
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bc { get; set; }
        public Guid RecordId { get ; init; }
        public int MyProperty { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
