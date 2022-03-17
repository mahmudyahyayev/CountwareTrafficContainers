using Convey.CQRS.Commands;
using Mhd.Framework.Common;

namespace CountwareTraffic.Workers.Sms.Application
{
    [Contract]
    public class SendTemplatedSms : ICommand
    {
        public ISmsTemplate Template { get; set; }
    }
}
