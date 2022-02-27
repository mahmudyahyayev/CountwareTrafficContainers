using Convey.CQRS.Commands;
using CountwareTraffic.WorkerServices.SignalrHub.Application;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Consumer
{
    public class SignalRDeviceChangedFailedConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceChangedFailed>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRDeviceChangedFailedConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceChangedFailed ququeEvent)
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
