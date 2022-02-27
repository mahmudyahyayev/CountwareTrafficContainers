using Convey.CQRS.Queries;
using CountwareTraffic.Services.Events.Application;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Common;
using Sensormatic.Tool.Queue;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api
{
    public class AutoCreateDeviceEventsConsumerSubsriber : BackgroundService
    {
        private readonly ILogger<AutoCreateDeviceEventsConsumerSubsriber> _logger;
        private readonly IQueueService _queueService;
        private readonly IQueryDispatcher _queryDispatcher;
        public AutoCreateDeviceEventsConsumerSubsriber(ILogger<AutoCreateDeviceEventsConsumerSubsriber> logger, IQueueService queueService, IQueryDispatcher queryDispatcher)
        {
            _logger = logger;
            _queueService = queueService;
            _queryDispatcher = queryDispatcher;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Eski aboneleri de ekle. Eger restart olunduysa.
            var devices = await _queryDispatcher.QueryAsync(new GetDevices { });
            foreach (var device in devices)
                _queueService.Subscribe<DeviceEventsListenerConsumer>(String.Format(Queues.CountwareTrafficEventsDeviceEventsListener, device.Name), QueueConfigTemplate.Default());
            

            _queueService.Subscribe<AutoCreateDeviceEventsConsumer>(Queues.CountwareTrafficEventsAutoCreateDeviceEventsConsumer, QueueConfigTemplate.Default());
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Auto Create Devie Events subscriber forcing to stop...");

            _queueService.StopConsumers();

            await Task.Delay(5000);

            await base.StopAsync(cancellationToken);
        }
    }
}
