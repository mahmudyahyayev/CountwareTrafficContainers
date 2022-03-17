using Convey.CQRS.Events;
using CountwareTraffic.Services.Events.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api
{
    public class CreateEventInElasticConsumer : IConsumer<Mhd.Framework.QueueModel.EventCreated>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public CreateEventInElasticConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Mhd.Framework.QueueModel.EventCreated queuEvent)
        {
            var subAreaDeleted = _eventMapper.Map(queuEvent) as EventCreated;
            await _eventDispatcher.PublishAsync(subAreaDeleted);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
