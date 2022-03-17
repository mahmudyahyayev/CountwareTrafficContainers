using Convey.CQRS.Commands;
using CountwareTraffic.Workers.SignalR.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Consumer
{
    public class SignalRSubAreaCreatedSuccessfullyConsumer : IConsumer<Mhd.Framework.QueueModel.SubAreaCreatedSuccessfully>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRSubAreaCreatedSuccessfullyConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SubAreaCreatedSuccessfully ququeEvent)
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
