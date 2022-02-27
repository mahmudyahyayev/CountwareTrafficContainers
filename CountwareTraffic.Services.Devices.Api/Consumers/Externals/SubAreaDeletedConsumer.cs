using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Application;
using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Api
{
    public class SubAreaDeletedConsumer : IConsumer<Sensormatic.Tool.QueueModel.SubAreaDeleted>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        private readonly ILogger<SubAreaDeletedConsumer> _logger;
        public SubAreaDeletedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper, ILogger<SubAreaDeletedConsumer> logger)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
            _logger = logger;
        }
        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SubAreaDeleted queuEvent)
        {
            _logger.LogInformation($"Consumed deleted SubArea with id {queuEvent.SubAreaId} with correlationId {queuEvent.CorrelationId}");
            var subAreaDeleted = _eventMapper.Map(queuEvent) as SubAreaDeleted;
            await _eventDispatcher.PublishAsync(subAreaDeleted);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
