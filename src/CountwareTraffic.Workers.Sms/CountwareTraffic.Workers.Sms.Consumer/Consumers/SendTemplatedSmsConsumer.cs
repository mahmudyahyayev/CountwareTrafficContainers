using Convey.CQRS.Commands;
using CountwareTraffic.Workers.Sms.Application;
using Mhd.Framework.Common;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Sms.Consumer
{
    public class SendTemplatedSmsConsumer :  IConsumer<Mhd.Framework.QueueModel.SendTemplatedSms> , ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public SendTemplatedSmsConsumer(ICommandDispatcher commandDispatcher)
           => _commandDispatcher = commandDispatcher;


        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SendTemplatedSms queueCommand)
        {
            string jsonValue = queueCommand.SmsTemplate.ToString();
            Type type = Type.GetType(queueCommand.SmsTemplateAssemblyName);
            dynamic template = jsonValue.Deserialize(type);

            await _commandDispatcher.SendAsync(new SendTemplatedSms { Template = template });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
