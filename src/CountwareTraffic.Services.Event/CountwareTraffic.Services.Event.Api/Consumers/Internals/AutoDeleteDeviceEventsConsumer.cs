using Mhd.Framework.Common;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api
{
    public class AutoDeleteDeviceEventsConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceDeletedSuccessfully>, ITransientSelfDependency
    {
        private readonly IQueueService _queueService;
        public AutoDeleteDeviceEventsConsumer(IQueueService queueService) => _queueService = queueService;

        public Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceDeletedSuccessfully queuEvent)
        {
            string queueName = string.Format(Queues.CountwareTrafficEventsDeviceEventsListener, queuEvent.DeviceName);

            _queueService.DeleteQueue(queueName);
            _queueService.DeleteExchange($"dlx.{queueName}");

            return Task.CompletedTask;
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
