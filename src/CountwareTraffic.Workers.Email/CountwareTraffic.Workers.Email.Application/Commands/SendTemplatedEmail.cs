using Convey.CQRS.Commands;
using Mhd.Framework.Common;

namespace CountwareTraffic.Workers.Email.Application
{
    [Contract]
    public class SendTemplatedEmail : ICommand
    {
        public IEmailTemplate Template { get; set; }
    }
}
