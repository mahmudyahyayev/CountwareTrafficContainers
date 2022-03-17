using Convey.CQRS.Commands;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;
using CountwareTraffic.Workers.SignalR.Application;

namespace CountwareTraffic.Workers.SignalR.Consumer
{
    public class SignalRDeviceCreatedSuccessfullyConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceCreatedSuccessfully>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRDeviceCreatedSuccessfullyConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceCreatedSuccessfully ququeEvent)
        {
            await _commandDispatcher.SendAsync(new DeviceCreatedSuccessfully
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
