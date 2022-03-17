using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Application;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Api
{
    public class SubAreaChangedConsumer : IConsumer<Mhd.Framework.QueueModel.SubAreaChanged>, ITransientSelfDependency
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
        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SubAreaChanged queuEvent)
        {
            _logger.LogInformation($"Consumed changed SubArea with name {queuEvent.Name} with correlationId {queuEvent.CorrelationId}");
            var subAreaChanged = _eventMapper.Map(queuEvent) as SubAreaChanged;
            await _eventDispatcher.PublishAsync(subAreaChanged);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
