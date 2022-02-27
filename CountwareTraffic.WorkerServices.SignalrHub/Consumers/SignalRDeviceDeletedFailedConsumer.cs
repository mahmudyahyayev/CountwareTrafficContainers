using Convey.CQRS.Commands;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using CountwareTraffic.WorkerServices.SignalrHub.Application;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Consumer
{
    public class SignalRDeviceDeletedFailedConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceDeletedFailed>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SignalRDeviceDeletedFailedConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceDeletedFailed ququeEvent)
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
