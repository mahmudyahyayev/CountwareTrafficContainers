using Convey.CQRS.Commands;
using CountwareTraffic.WorkerServices.Email.Application;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Email.Consumer
{
    public class SendEmailConsumer : IConsumer<Sensormatic.Tool.QueueModel.SendEmail>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;
        
        public SendEmailConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SendEmail queueCommand)
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
