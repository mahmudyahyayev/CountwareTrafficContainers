using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sensormatic.Tool.Queue
{
    public class AutoScaler : BackgroundService
    {
        private readonly ILogger<AutoScaler> _logger;
        private readonly IQueueService _queueService;

        public AutoScaler(ILogger<AutoScaler> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(3));

                _queueService.AutoScale();
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Auto scaler service stopped");

            return base.StopAsync(cancellationToken);
        }
    }
}
