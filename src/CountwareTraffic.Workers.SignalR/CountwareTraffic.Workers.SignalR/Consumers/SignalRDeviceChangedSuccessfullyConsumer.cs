using Convey.CQRS.Commands;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;
using CountwareTraffic.Workers.SignalR.Application;

namespace CountwareTraffic.Workers.SignalR.Consumer
{
    public class SignalRDeviceChangedSuccessfullyConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceChangedSuccessfully>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRDeviceChangedSuccessfullyConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceChangedSuccessfully ququeEvent)
        {
            await _commandDispatcher.SendAsync(new DeviceChangedSuccessfully
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
