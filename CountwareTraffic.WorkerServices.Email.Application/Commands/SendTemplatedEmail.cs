using Convey.CQRS.Commands;
using Sensormatic.Tool.Common;

namespace CountwareTraffic.WorkerServices.Email.Application
{
    [Contract]
    public class SendTemplatedEmail : ICommand
    {
        public IEmailTemplate Template { get; set; }
    }
}
