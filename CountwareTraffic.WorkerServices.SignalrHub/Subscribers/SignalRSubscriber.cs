using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Common;
using Sensormatic.Tool.Queue;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Consumer
{
    public class SignalRSubscriber : BackgroundService
    {
        private readonly ILogger<SignalRSubscriber> _logger;
        private readonly IQueueService _queueService;
        public SignalRSubscriber(ILogger<SignalRSubscriber> logger, IQueueService queueService)
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
                ExcludeExceptions = new List<Type> { typeof(Exception), typeof(HttpRequestException), typeof(ArgumentNullException) },
                AutoScale = true,
                ScaleUpTo = 10
            };

            _queueService.Subscribe<SignalRSubAreaCreatedSuccessfullyConsumer>(Queues.CountwareTrafficSignalRSubAreaCreatedSuccessfully, queueConfigTemplate);
            _queueService.Subscribe<SignalRSubAreaCreatedFailedConsumer>(Queues.CountwareTrafficSignalRSubAreaCreatedFailed, queueConfigTemplate);
            _queueService.Subscribe<SignalRSubAreaChangedSuccessfullyConsumer>(Queues.CountwareTrafficSignalRSubAreaChangedSuccessfully, queueConfigTemplate);
            _queueService.Subscribe<SignalRSubAreaChangedFailedConsumer>(Queues.CountwareTrafficSignalRSubAreaChangedFailed, queueConfigTemplate);
            _queueService.Subscribe<SignalRSubAreaDeletedSuccessfullyConsumer>(Queues.CountwareTrafficSignalRSubAreaDeletedSuccessfully, queueConfigTemplate);
            _queueService.Subscribe<SignalRSubAreaDeletedFailedConsumer>(Queues.CountwareTrafficSignalRSubAreaDeletedFailed, queueConfigTemplate);

            _queueService.Subscribe<SignalRDeviceCreatedSuccessfullyConsumer>(Queues.CountwareTrafficSignalRDeviceCreatedSuccessfully, queueConfigTemplate);
            _queueService.Subscribe<SignalRDeviceCreatedFailedConsumer>(Queues.CountwareTrafficSignalRDeviceCreatedFailed, queueConfigTemplate);
            _queueService.Subscribe<SignalRDeviceChangedSuccessfullyConsumer>(Queues.CountwareTrafficSignalRDeviceChangedSuccessfully, queueConfigTemplate);
            _queueService.Subscribe<SignalRDeviceChangedFailedConsumer>(Queues.CountwareTrafficSignalRDeviceChangedFailed, queueConfigTemplate);
            _queueService.Subscribe<SignalRDeviceDeletedSuccessfullyConsumer>(Queues.CountwareTrafficSignalRDeviceDeletedSuccessfully, queueConfigTemplate);
            _queueService.Subscribe<SignalRDeviceDeletedFailedConsumer>(Queues.CountwareTrafficSignalRDeviceDeletedFailed, queueConfigTemplate);
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("SignalrHub Consumer forcing to stop...");

            _queueService.StopConsumers();

            await Task.Delay(5000);

            await base.StopAsync(cancellationToken);
        }
    }
}
