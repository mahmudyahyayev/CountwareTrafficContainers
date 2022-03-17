using Convey.CQRS.Commands;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using CountwareTraffic.Workers.SignalR.Application;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Consumer
{
    public class SignalRDeviceDeletedFailedConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceDeletedFailed>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRDeviceDeletedFailedConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceDeletedFailed ququeEvent)
        {
            await _commandDispatcher.SendAsync(new DeviceDeletedFailed
            {
                DeviceId = ququeEvent.DeviceId,
                IsDeleted = ququeEvent.IsDeleted,
                UserId = ququeEvent.UserId,
                UserName = ququeEvent.UserName
            });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
