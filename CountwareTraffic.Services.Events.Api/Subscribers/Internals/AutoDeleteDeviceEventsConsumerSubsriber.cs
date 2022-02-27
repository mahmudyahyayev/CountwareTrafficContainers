using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Common;
using Sensormatic.Tool.Queue;
using System.Threading;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api
{
    public class AutoDeleteDeviceEventsConsumerSubsriber : BackgroundService
    {
        private readonly ILogger<AutoDeleteDeviceEventsConsumerSubsriber> _logger;
        private readonly IQueueService _queueService;
        public AutoDeleteDeviceEventsConsumerSubsriber(ILogger<AutoDeleteDeviceEventsConsumerSubsriber> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _queueService.Subscribe<AutoDeleteDeviceEventsConsumer>(Queues.CountwareTrafficEventsAutoDeleteDeviceEventsConsumer, QueueConfigTemplate.Default());
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Auto Delete Devie Events subscriber forcing to stop...");

            _queueService.StopConsumers();

            await Task.Delay(5000);

            await base.StopAsync(cancellationToken);
        }
    }

}
