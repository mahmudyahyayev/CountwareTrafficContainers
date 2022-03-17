using Convey.CQRS.Queries;
using CountwareTraffic.Services.Events.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;
using Mhd.Framework.Common;
using Mhd.Framework.Queue;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Listener2
{
    public class AutoDeleteDeviceEventJobSubscriber : BackgroundService
    {
        private readonly ILogger<AutoDeleteDeviceEventJobSubscriber> _logger;
        private readonly IQueueService _queueService;

        public IScheduler Scheduler { get; set; }
        public AutoDeleteDeviceEventJobSubscriber(ILogger<AutoDeleteDeviceEventJobSubscriber> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _queueService.Subscribe<AutoDeleteDeviceEventsJobConsumer>(Queues.CountwareTrafficEventsAutoDeleteDeviceEventsJob, QueueConfigTemplate.Default());
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Auto Delete Device Events job subscriber forcing to stop...");

            _queueService.StopConsumers();

            await Task.Delay(5000);

            await base.StopAsync(cancellationToken);
        }
    }
}
