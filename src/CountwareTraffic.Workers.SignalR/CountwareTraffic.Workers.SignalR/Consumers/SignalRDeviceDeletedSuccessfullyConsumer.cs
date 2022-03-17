using Convey.CQRS.Commands;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;
using CountwareTraffic.Workers.SignalR.Application;

namespace CountwareTraffic.Workers.SignalR.Consumer
{
    public class SignalRDeviceDeletedSuccessfullyConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceDeletedSuccessfully>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRDeviceDeletedSuccessfullyConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceDeletedSuccessfully ququeEvent)
        {
            await _commandDispatcher.SendAsync(new DeviceDeletedSuccessfully
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
