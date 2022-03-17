using Convey.CQRS.Commands;
using CountwareTraffic.Workers.SignalR.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Consumer
{
    public class SignalRSubAreaDeletedSuccessfullyConsumer : IConsumer<Mhd.Framework.QueueModel.SubAreaDeletedSuccessfully>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRSubAreaDeletedSuccessfullyConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SubAreaDeletedSuccessfully ququeEvent)
        {
            await _commandDispatcher.SendAsync(new SubAreaDeletedSuccessfully
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
