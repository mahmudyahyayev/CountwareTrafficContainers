using Convey.CQRS.Events;
using CountwareTraffic.Services.Areas.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Api
{
    public class SubAreaDeletedRejectedConsumer : IConsumer<Mhd.Framework.QueueModel.SubAreaDeletedRejected>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public SubAreaDeletedRejectedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SubAreaDeletedRejected queuEvent)
        {
            var subAreaCreatedRejected = _eventMapper.Map(queuEvent) as SubAreaDeletedRejected;
            await _eventDispatcher.PublishAsync(subAreaCreatedRejected);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
