using Mhd.Framework.Queue;
using System;
using System.Collections.Generic;

namespace Mhd.Framework.QueueModel
{
    public record SendSms : IQueueCommand
    {
        public string Message { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public List<Guid> UserIds { get; set; }
        public bool IsOtp { get; set; }
        public Guid RecordId { get ; init; }
    }
}
