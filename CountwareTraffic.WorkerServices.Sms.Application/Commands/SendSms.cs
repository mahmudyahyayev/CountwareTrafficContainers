using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.WorkerServices.Sms.Application
{
    [ContractAttribute]
    public class SendSms : ICommand
    {
        public string Message { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public List<Guid> UserIds { get; set; }
        public bool IsOtp { get; set; }
    }
}
