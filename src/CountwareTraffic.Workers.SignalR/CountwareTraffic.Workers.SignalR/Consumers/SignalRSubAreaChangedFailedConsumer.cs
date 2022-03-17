using Convey.CQRS.Commands;
using CountwareTraffic.Workers.SignalR.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Consumer
{
    public class SignalRSubAreaChangedFailedConsumer : IConsumer<Mhd.Framework.QueueModel.SubAreaChangedFailed>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRSubAreaChangedFailedConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SubAreaChangedFailed ququeEvent)
        {
            await _commandDispatcher.SendAsync(new SubAreaChangedFailed
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
