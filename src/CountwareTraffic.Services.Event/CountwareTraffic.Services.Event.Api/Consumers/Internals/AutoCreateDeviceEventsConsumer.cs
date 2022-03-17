using Convey.CQRS.Commands;
using Mhd.Framework.Common;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api
{
    public class AutoCreateDeviceEventsConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceCreatedCompleted>, ITransientSelfDependency
    {
        private readonly IQueueService _queueService;
        public AutoCreateDeviceEventsConsumer(IQueueService queueService) => _queueService = queueService;

        public Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceCreatedCompleted queuEvent)
        {
            _queueService.Subscribe<DeviceEventsListenerConsumer>(String.Format(Queues.CountwareTrafficEventsDeviceEventsListener, queuEvent.Name), QueueConfigTemplate.Default());

            return Task.CompletedTask;
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
