using Convey.CQRS.Events;
using CountwareTraffic.Services.Companies.Application;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Api
{
    public class SubAreaCreatedRejectedConsumer : IConsumer<Sensormatic.Tool.QueueModel.SubAreaCreatedRejected>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public SubAreaCreatedRejectedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SubAreaCreatedRejected queuEvent)
        {
            var subAreaCreatedRejected = _eventMapper.Map(queuEvent) as SubAreaCreatedRejected;
            await _eventDispatcher.PublishAsync(subAreaCreatedRejected);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
