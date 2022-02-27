using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Application;
using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Api
{
    public class SubAreaChangedConsumer : IConsumer<Sensormatic.Tool.QueueModel.SubAreaChanged>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        private readonly ILogger<SubAreaChangedConsumer> _logger;
        public SubAreaChangedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper, ILogger<SubAreaChangedConsumer> logger)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
            _logger = logger;
        }
        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SubAreaChanged queuEvent)
        {
            _logger.LogInformation($"Consumed changed SubArea with name {queuEvent.Name} with correlationId {queuEvent.CorrelationId}");
            var subAreaChanged = _eventMapper.Map(queuEvent) as SubAreaChanged;
            await _eventDispatcher.PublishAsync(subAreaChanged);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
