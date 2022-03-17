using Convey.CQRS.Commands;
using CountwareTraffic.Workers.SignalR.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Consumer
{
    public class SignalRSubAreaCreatedFailedConsumer : IConsumer<Mhd.Framework.QueueModel.SubAreaCreatedFailed>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRSubAreaCreatedFailedConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SubAreaCreatedFailed ququeEvent)
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
