using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Common;
using Mhd.Framework.Queue;
using System.Threading;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Listener
{
    public class AutoDeleteDeviceEventEndpointSubscriber : BackgroundService
    {
        private readonly ILogger<AutoDeleteDeviceEventEndpointSubscriber> _logger;
        private readonly IQueueService _queueService;

        public AutoDeleteDeviceEventEndpointSubscriber(ILogger<AutoDeleteDeviceEventEndpointSubscriber> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _queueService.Subscribe<AutoDeleteDeviceEventsEndpointConsumer>(Queues.CountwareTrafficEventsAutoDeleteDeviceEventsJob, QueueConfigTemplate.Default());
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Auto Delete Device Events Endpoint subscriber forcing to stop...");

            _queueService.StopConsumers();

            await Task.Delay(5000);

            await base.StopAsync(cancellationToken);
        }
    }
}
