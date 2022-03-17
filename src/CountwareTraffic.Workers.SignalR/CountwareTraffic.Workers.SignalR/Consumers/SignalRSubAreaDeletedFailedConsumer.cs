using Convey.CQRS.Commands;
using CountwareTraffic.Workers.SignalR.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Consumer
{
    public class  SignalRSubAreaDeletedFailedConsumer : IConsumer<Mhd.Framework.QueueModel.SubAreaDeletedFailed>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRSubAreaDeletedFailedConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SubAreaDeletedFailed ququeEvent)
        {
            await _commandDispatcher.SendAsync(new SubAreaDeletedFailed
            {
                SubAreaId = ququeEvent.SubAreaId,
                IsDeleted = ququeEvent.IsDeleted,
                UserId = ququeEvent.UserId,
                UserName = ququeEvent.UserName
            });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
