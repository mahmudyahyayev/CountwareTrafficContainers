using Convey.CQRS.Commands;
using CountwareTraffic.WorkerServices.Sms.Application;
using Sensormatic.Tool.Common;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Sms.Consumer
{
    public class SendTemplatedSmsConsumer :  IConsumer<Sensormatic.Tool.QueueModel.SendTemplatedSms> , ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public SendTemplatedSmsConsumer(ICommandDispatcher commandDispatcher)
           => _commandDispatcher = commandDispatcher;


        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SendTemplatedSms queueCommand)
        {
            string jsonValue = queueCommand.SmsTemplate.ToString();
            Type type = Type.GetType(queueCommand.SmsTemplateAssemblyName);
            dynamic template = jsonValue.Deserialize(type);

            await _commandDispatcher.SendAsync(new SendTemplatedSms { Template = template });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
