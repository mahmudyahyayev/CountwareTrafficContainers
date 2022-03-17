using Convey.CQRS.Commands;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using CountwareTraffic.Workers.SignalR.Application;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Consumer
{
    public class SignalRDeviceCreatedFailedConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceCreatedFailed>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRDeviceCreatedFailedConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceCreatedFailed ququeEvent)
        {
            await _commandDispatcher.SendAsync(new DeviceCreatedFailed
            {
                DeviceId = ququeEvent.DeviceId,
                DeviceCreationStatus = ququeEvent.DeviceCreationStatus,
                UserId = ququeEvent.UserId,
                UserName = ququeEvent.UserName
            });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
