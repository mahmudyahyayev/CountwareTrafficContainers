using Convey.CQRS.Commands;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using CountwareTraffic.WorkerServices.SignalrHub.Application;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Consumer
{
    public class SignalRDeviceCreatedFailedConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceCreatedFailed>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRDeviceCreatedFailedConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceCreatedFailed ququeEvent)
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
