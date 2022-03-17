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
    public class DeviceSubscriber : BackgroundService
    {
        private readonly IQueueService _queueService;
        private readonly ILogger<DeviceSubscriber> _logger;

        public DeviceSubscriber(ILogger<DeviceSubscriber> logger, IQueueService queueService)
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
                ExcludeExceptions = new List<Type> { typeof(Exception), typeof(HttpRequestException), typeof(ArgumentNullException) },
                AutoScale = true,
                ScaleUpTo = 10
            };

            _queueService.Subscribe<DeviceCreatedCompletedConsumer>(Queues.CountwareTrafficDevicesDeviceCreatedCompleted, template);
            _queueService.Subscribe<DeviceCreatedRejectedConsumer>(Queues.CountwareTrafficDevicesDeviceCreatedRejected, template);
            _queueService.Subscribe<DeviceDeletedRejectedConsumer>(Queues.CountwareTrafficDevicesDeviceDeletedRejected, template);
            _queueService.Subscribe<DeviceChangedRejectedConsumer>(Queues.CountwareTrafficDevicesDeviceChangedRejected, template);
            _queueService.Subscribe<DeviceStatusChangedConsumer>(Queues.CountwareTrafficDevicesDeviceStatusChanged, template);

            return Task.CompletedTask;
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Device subscriber forcing to stop...");

            _queueService.StopConsumers();

            await Task.Delay(5000);

            await base.StopAsync(cancellationToken);
        }
    }
}
