using Convey.CQRS.Commands;
using CountwareTraffic.Workers.Sms.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Sms.Consumer
{
    public class SendSmsConsumer : IConsumer<Mhd.Framework.QueueModel.SendSms>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;
        
        public SendSmsConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SendSms queueCommand)
        {
            await _commandDispatcher.SendAsync(new SendSms
            {
                IsOtp = queueCommand.IsOtp,
                Message = queueCommand.Message,
                PhoneNumbers = queueCommand.PhoneNumbers,
                UserIds = queueCommand.UserIds
            });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
