using Convey.CQRS.Commands;
using CountwareTraffic.WorkerServices.SignalrHub.Application;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Consumer
{
    public class SignalRSubAreaChangedSuccessfullyConsumer : IConsumer<Sensormatic.Tool.QueueModel.SubAreaChangedSuccessfully>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRSubAreaChangedSuccessfullyConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SubAreaChangedSuccessfully ququeEvent)
        {
            await _commandDispatcher.SendAsync(new SubAreaChangedSuccessfully
            {
                SubAreaId = ququeEvent.SubAreaId,
                NewName = ququeEvent.NewName,
                OldName = ququeEvent.OldName,
                UserId = ququeEvent.UserId,
                UserName = ququeEvent.UserName
            });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
