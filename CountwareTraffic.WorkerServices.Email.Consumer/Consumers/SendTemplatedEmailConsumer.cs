using Convey.CQRS.Commands;
using CountwareTraffic.WorkerServices.Email.Application;
using Sensormatic.Tool.Common;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Email.Consumer
{
    public class SendTemplatedEmailConsumer :  IConsumer<Sensormatic.Tool.QueueModel.SendTemplatedEmail> , ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public SendTemplatedEmailConsumer(ICommandDispatcher commandDispatcher)
           => _commandDispatcher = commandDispatcher;


        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SendTemplatedEmail queueCommand)
        {
            string jsonValue = queueCommand.EmailTemplate.ToString();
            Type type = Type.GetType(queueCommand.EmailTemplateAssemblyName);
            dynamic template = jsonValue.Deserialize(type);

            await _commandDispatcher.SendAsync(new SendTemplatedEmail { Template = template });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
