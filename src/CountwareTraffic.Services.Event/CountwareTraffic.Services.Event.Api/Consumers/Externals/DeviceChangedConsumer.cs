using Convey.CQRS.Events;
using CountwareTraffic.Services.Events.Application;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api
{
    public class DeviceChangedConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceChanged>, ITransientSelfDependency
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

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceChanged queueEvent)
        {
            _logger.LogInformation($"Consumed change Device with name {queueEvent.Name} with correlationId {queueEvent.CorrelationId}");
            var deviceChanged = _eventMapper.Map(queueEvent) as DeviceChanged;
            await _eventDispatcher.PublishAsync(deviceChanged);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}

    
