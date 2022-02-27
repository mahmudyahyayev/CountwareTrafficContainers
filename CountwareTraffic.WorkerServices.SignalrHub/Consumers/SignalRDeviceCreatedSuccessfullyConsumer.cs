using Convey.CQRS.Commands;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;
using CountwareTraffic.WorkerServices.SignalrHub.Application;

namespace CountwareTraffic.WorkerServices.SignalrHub.Consumer
{
    public class SignalRDeviceCreatedSuccessfullyConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceCreatedSuccessfully>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRDeviceCreatedSuccessfullyConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceCreatedSuccessfully ququeEvent)
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
