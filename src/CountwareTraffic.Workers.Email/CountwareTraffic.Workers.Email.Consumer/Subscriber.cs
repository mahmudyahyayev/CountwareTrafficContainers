using CountwareTraffic.Workers.Email.Application;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Common;
using Mhd.Framework.Queue;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Email.Consumer
{
    public class Subscriber : BackgroundService
    {
        private readonly ILogger<Subscriber> _logger;
        private readonly IQueueService _queueService;
        public Subscriber(ILogger<Subscriber> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueConfigTemplate = new QueueConfigTemplate
            {
                PrefetchCount = 1,
                RetryCount = 2,
                RetryIntervalSeconds = 120,
                ExcludeExceptions = new List<Type> { typeof(Exception), typeof(HttpRequestException), typeof(ArgumentNullException), typeof(EmailTemplateNotFoundException), typeof(DbLogException) },
                AutoScale = true,
                ScaleUpTo = 10
            };

            _queueService.Subscribe<SendTemplatedEmailConsumer>(Queues.CountwareTrafficSendTemplatedEmail, queueConfigTemplate);
            _queueService.Subscribe<SendEmailConsumer>(Queues.CountwareTrafficSendEmail, queueConfigTemplate);
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Email Consumer forcing to stop...");

            _queueService.StopConsumers();

            await Task.Delay(5000);

            await base.StopAsync(cancellationToken);
        }
    }
}
