using Convey.CQRS.Commands;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;
using CountwareTraffic.WorkerServices.SignalrHub.Application;

namespace CountwareTraffic.WorkerServices.SignalrHub.Consumer
{
    public class SignalRDeviceDeletedSuccessfullyConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceDeletedSuccessfully>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRDeviceDeletedSuccessfullyConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceDeletedSuccessfully ququeEvent)
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
