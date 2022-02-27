using Convey.CQRS.Commands;
using Sensormatic.Tool.Common;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api
{
    public class AutoCreateDeviceEventsConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceCreatedCompleted>, ITransientSelfDependency
    {
        private readonly IQueueService _queueService;
        public AutoCreateDeviceEventsConsumer(IQueueService queueService) => _queueService = queueService;

        public Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceCreatedCompleted queuEvent)
        {
            _queueService.Subscribe<DeviceEventsListenerConsumer>(String.Format(Queues.CountwareTrafficEventsDeviceEventsListener, queuEvent.Name), QueueConfigTemplate.Default());

            return Task.CompletedTask;
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
