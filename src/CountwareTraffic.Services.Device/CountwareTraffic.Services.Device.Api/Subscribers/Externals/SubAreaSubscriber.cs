using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Common;
using Mhd.Framework.Queue;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Api
{
    public class SubAreaSubscriber : BackgroundService
    {
        private readonly IQueueService _queueService;
        private readonly ILogger<SubAreaSubscriber> _logger;

        public SubAreaSubscriber(ILogger<SubAreaSubscriber> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var template = new QueueConfigTemplate
            {
                PrefetchCount = 1,
                RetryCount = 0,
                RetryIntervalSeconds = 0,
                ExcludeExceptions  = new List<Type> { typeof(Exception), typeof(HttpRequestException), typeof(ArgumentNullException) },
                AutoScale = true,
                ScaleUpTo = 10
            };

            _queueService.Subscribe<SubAreaCreatedConsumer>(Queues.CountwareTrafficAreasSubAreaCreated, template);
            _queueService.Subscribe<SubAreaChangedConsumer>(Queues.CountwareTrafficAreasSubAreaChanged, template);
            _queueService.Subscribe<SubAreaDeletedConsumer>(Queues.CountwareTrafficAreasSubAreaDeleted, template);

            return Task.CompletedTask;
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("SubArea forcing to stop...");

            _queueService.StopConsumers();

            await Task.Delay(5000);

            await base.StopAsync(cancellationToken);
        }
    }
}
