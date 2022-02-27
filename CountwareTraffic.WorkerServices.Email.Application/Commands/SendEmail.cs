using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.WorkerServices.Email.Application
{
    [Contract]
    public class SendEmail : ICommand
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public IList<string> To { get; set; }
        public IList<string> Cc { get; set; }
        public IList<string> Bc { get; set; }
        public IList<Guid> UserId { get; set; }
    }
}
