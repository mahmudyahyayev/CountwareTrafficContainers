using Convey.CQRS.Commands;
using CountwareTraffic.WorkerServices.SignalrHub.Application;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Consumer
{
    public class SignalRSubAreaCreatedSuccessfullyConsumer : IConsumer<Sensormatic.Tool.QueueModel.SubAreaCreatedSuccessfully>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRSubAreaCreatedSuccessfullyConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SubAreaCreatedSuccessfully ququeEvent)
        {
            await _commandDispatcher.SendAsync(new SubAreaCreatedSuccessfully
            {
                SubAreaId = ququeEvent.SubAreaId,
                SubAreaStatus = ququeEvent.SubAreaStatus,
                UserId = ququeEvent.UserId,
                UserName = ququeEvent.UserName
            });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
