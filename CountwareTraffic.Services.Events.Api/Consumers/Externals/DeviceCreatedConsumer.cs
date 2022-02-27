using Convey.CQRS.Events;
using CountwareTraffic.Services.Events.Application;
using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api
{
    public class DeviceCreatedConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceCreated>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        private readonly ILogger<DeviceCreatedConsumer> _logger;
        public DeviceCreatedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper, ILogger<DeviceCreatedConsumer> logger)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
            _logger = logger;
        }

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceCreated queueModel)
        {
            _logger.LogInformation($"Consumed create Device with name {queueModel.Name} with correlationId {queueModel.CorrelationId}");
            var deviceCreated = _eventMapper.Map(queueModel) as DeviceCreated;
            await _eventDispatcher.PublishAsync(deviceCreated);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
