using Convey.CQRS.Commands;
using CountwareTraffic.WorkerServices.SignalrHub.Application;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Consumer
{
    public class SignalRSubAreaCreatedFailedConsumer : IConsumer<Sensormatic.Tool.QueueModel.SubAreaCreatedFailed>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRSubAreaCreatedFailedConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SubAreaCreatedFailed ququeEvent)
        {
            await _commandDispatcher.SendAsync(new SubAreaCreatedFailed
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
