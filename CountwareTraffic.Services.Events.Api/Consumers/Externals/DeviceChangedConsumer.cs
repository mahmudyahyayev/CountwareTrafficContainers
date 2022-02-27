using Convey.CQRS.Events;
using CountwareTraffic.Services.Events.Application;
using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api
{
    public class DeviceChangedConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceChanged>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        private readonly ILogger<DeviceChangedConsumer> _logger;
        public DeviceChangedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper, ILogger<DeviceChangedConsumer> logger)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
            _logger = logger;
        }

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceChanged queueEvent)
        {
            _logger.LogInformation($"Consumed change Device with name {queueEvent.Name} with correlationId {queueEvent.CorrelationId}");
            var deviceChanged = _eventMapper.Map(queueEvent) as DeviceChanged;
            await _eventDispatcher.PublishAsync(deviceChanged);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}

    
