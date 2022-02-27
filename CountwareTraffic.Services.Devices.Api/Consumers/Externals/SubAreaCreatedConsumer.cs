using Convey.CQRS.Events;
using Sensormatic.Tool.Queue;
using System.Threading.Tasks;
using Sensormatic.Tool.Ioc;
using CountwareTraffic.Services.Devices.Application;
using Microsoft.Extensions.Logging;

namespace CountwareTraffic.Services.Devices.Api
{
    public class SubAreaCreatedConsumer : IConsumer<Sensormatic.Tool.QueueModel.SubAreaCreated>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        private readonly ILogger<SubAreaCreatedConsumer> _logger;
        public SubAreaCreatedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper, ILogger<SubAreaCreatedConsumer> logger)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
            _logger = logger;
        }
        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SubAreaCreated queuEvent)
        {
            _logger.LogInformation($"Consumed created SubArea with name {queuEvent.Name} with correlationId {queuEvent.CorrelationId}");
            var subAreaCreated = _eventMapper.Map(queuEvent) as SubAreaCreated;
            await _eventDispatcher.PublishAsync(subAreaCreated);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
