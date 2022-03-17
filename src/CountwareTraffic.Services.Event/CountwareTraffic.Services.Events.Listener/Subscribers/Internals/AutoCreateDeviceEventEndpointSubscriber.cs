using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Common;
using Mhd.Framework.Queue;
using System.Threading;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Listener
{
    public class AutoCreateDeviceEventEndpointSubscriber : BackgroundService
    {
        private readonly ILogger<AutoCreateDeviceEventEndpointSubscriber> _logger;
        private readonly IQueueService _queueService;

        public AutoCreateDeviceEventEndpointSubscriber(ILogger<AutoCreateDeviceEventEndpointSubscriber> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _queueService.Subscribe<AutoCreateDeviceEventsEndpointConsumer>(Queues.CountwareTrafficEventsAutoCreateDeviceEventsEndpoint, QueueConfigTemplate.Default());
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Auto Create Device Events Endpoint subscriber forcing to stop...");

            _queueService.StopConsumers();

            await Task.Delay(5000);

            await base.StopAsync(cancellationToken);
        }
    }
}
