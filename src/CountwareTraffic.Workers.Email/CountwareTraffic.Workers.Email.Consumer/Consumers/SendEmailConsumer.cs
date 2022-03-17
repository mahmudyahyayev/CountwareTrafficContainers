using Convey.CQRS.Commands;
using CountwareTraffic.Workers.Email.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Email.Consumer
{
    public class SendEmailConsumer : IConsumer<Mhd.Framework.QueueModel.SendEmail>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;
        
        public SendEmailConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SendEmail queueCommand)
        {
            await _commandDispatcher.SendAsync(new SendEmail
            {
                Bc = queueCommand.Bc,
                Body = queueCommand.Body,
                Cc = queueCommand.Cc,
                IsHtml = queueCommand.IsHtml,
                Subject = queueCommand.Subject,
                To = queueCommand.To,
                UserId = queueCommand.UserIds,
            });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
