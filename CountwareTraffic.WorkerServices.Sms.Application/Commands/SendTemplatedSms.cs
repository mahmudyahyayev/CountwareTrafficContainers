using Convey.CQRS.Commands;
using Sensormatic.Tool.Common;

namespace CountwareTraffic.WorkerServices.Sms.Application
{
    [Contract]
    public class SendTemplatedSms : ICommand
    {
        public ISmsTemplate Template { get; set; }
    }
}
