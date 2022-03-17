using Convey.CQRS.Events;
using Mhd.Framework.Queue;
using System.Threading.Tasks;
using Mhd.Framework.Ioc;
using CountwareTraffic.Services.Devices.Application;
using Microsoft.Extensions.Logging;

namespace CountwareTraffic.Services.Devices.Api
{
    public class SubAreaCreatedConsumer : IConsumer<Mhd.Framework.QueueModel.SubAreaCreated>, ITransientSelfDependency
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
        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SubAreaCreated queuEvent)
        {
            _logger.LogInformation($"Consumed created SubArea with name {queuEvent.Name} with correlationId {queuEvent.CorrelationId}");
            var subAreaCreated = _eventMapper.Map(queuEvent) as SubAreaCreated;
            await _eventDispatcher.PublishAsync(subAreaCreated);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
