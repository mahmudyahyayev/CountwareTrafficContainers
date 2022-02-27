using Convey.CQRS.Events;
using CountwareTraffic.Services.Companies.Application;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Api
{
    public class SubAreaChangedRejectedConsumer : IConsumer<Sensormatic.Tool.QueueModel.SubAreaChangedRejected>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public SubAreaChangedRejectedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SubAreaChangedRejected queuEvent)
        {
            var subAreaChangedRejected = _eventMapper.Map(queuEvent) as SubAreaChangedRejected;
            await _eventDispatcher.PublishAsync(subAreaChangedRejected);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
