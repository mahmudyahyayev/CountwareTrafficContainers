using Sensormatic.Tool.Common;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api
{
    public class AutoDeleteDeviceEventsConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceDeletedSuccessfully>, ITransientSelfDependency
    {
        private readonly IQueueService _queueService;
        public AutoDeleteDeviceEventsConsumer(IQueueService queueService) => _queueService = queueService;

        public Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceDeletedSuccessfully queuEvent)
        {
            string queueName = string.Format(Queues.CountwareTrafficEventsDeviceEventsListener, queuEvent.DeviceName);

            _queueService.DeleteQueue(queueName);
            _queueService.DeleteExchange($"dlx.{queueName}");

            return Task.CompletedTask;
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
