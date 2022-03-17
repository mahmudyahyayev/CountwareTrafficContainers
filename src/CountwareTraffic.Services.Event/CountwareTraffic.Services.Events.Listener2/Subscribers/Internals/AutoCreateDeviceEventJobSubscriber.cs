using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Mhd.Framework.Common;
using Mhd.Framework.Queue;
using System.Threading;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Listener2
{
    public class AutoCreateDeviceEventJobSubscriber : BackgroundService
    {
        private readonly ILogger<AutoCreateDeviceEventJobSubscriber> _logger;
        private readonly IQueueService _queueService;

        public IScheduler Scheduler { get; set; }
        public AutoCreateDeviceEventJobSubscriber(ILogger<AutoCreateDeviceEventJobSubscriber> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _queueService.Subscribe<AutoCreateDeviceEventsJobConsumer>(Queues.CountwareTrafficEventsAutoCreateDeviceEventsJob, QueueConfigTemplate.Default());
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Auto Create Device Events job subscriber forcing to stop...");

            _queueService.StopConsumers();

            await Task.Delay(5000);

            await base.StopAsync(cancellationToken);
        }
    }
}
