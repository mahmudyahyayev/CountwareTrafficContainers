using Convey.CQRS.Commands;
using CountwareTraffic.Workers.SignalR.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Consumer
{
    public class SignalRDeviceChangedFailedConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceChangedFailed>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRDeviceChangedFailedConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceChangedFailed ququeEvent)
        {
            await _commandDispatcher.SendAsync(new DeviceChangedFailed
            {
                DeviceId = ququeEvent.DeviceId,
                NewName = ququeEvent.NewName,
                OldName = ququeEvent.OldName,
                UserId = ququeEvent.UserId,
                UserName = ququeEvent.UserName
            });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
