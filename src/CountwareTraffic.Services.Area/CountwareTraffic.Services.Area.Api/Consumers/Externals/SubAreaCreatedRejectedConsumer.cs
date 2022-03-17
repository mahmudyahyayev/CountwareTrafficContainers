using Convey.CQRS.Events;
using CountwareTraffic.Services.Areas.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Api
{
    public class SubAreaCreatedRejectedConsumer : IConsumer<Mhd.Framework.QueueModel.SubAreaCreatedRejected>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public SubAreaCreatedRejectedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SubAreaCreatedRejected queuEvent)
        {
            var subAreaCreatedRejected = _eventMapper.Map(queuEvent) as SubAreaCreatedRejected;
            await _eventDispatcher.PublishAsync(subAreaCreatedRejected);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
