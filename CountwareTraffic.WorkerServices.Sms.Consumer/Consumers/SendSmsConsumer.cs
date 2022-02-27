using Convey.CQRS.Commands;
using CountwareTraffic.WorkerServices.Sms.Application;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Sms.Consumer
{
    public class SendSmsConsumer : IConsumer<Sensormatic.Tool.QueueModel.SendSms>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;
        
        public SendSmsConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SendSms queueCommand)
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
