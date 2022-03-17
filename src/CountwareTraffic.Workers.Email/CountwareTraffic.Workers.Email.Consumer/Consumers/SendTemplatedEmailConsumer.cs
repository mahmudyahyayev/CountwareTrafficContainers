using Convey.CQRS.Commands;
using CountwareTraffic.Workers.Email.Application;
using Mhd.Framework.Common;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Email.Consumer
{
    public class SendTemplatedEmailConsumer :  IConsumer<Mhd.Framework.QueueModel.SendTemplatedEmail> , ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public SendTemplatedEmailConsumer(ICommandDispatcher commandDispatcher)
           => _commandDispatcher = commandDispatcher;


        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SendTemplatedEmail queueCommand)
        {
            string jsonValue = queueCommand.EmailTemplate.ToString();
            Type type = Type.GetType(queueCommand.EmailTemplateAssemblyName);
            dynamic template = jsonValue.Deserialize(type);

            await _commandDispatcher.SendAsync(new SendTemplatedEmail { Template = template });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
