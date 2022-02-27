using Convey.CQRS.Commands;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;
using CountwareTraffic.WorkerServices.SignalrHub.Application;

namespace CountwareTraffic.WorkerServices.SignalrHub.Consumer
{
    public class SignalRDeviceChangedSuccessfullyConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceChangedSuccessfully>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRDeviceChangedSuccessfullyConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceChangedSuccessfully ququeEvent)
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
