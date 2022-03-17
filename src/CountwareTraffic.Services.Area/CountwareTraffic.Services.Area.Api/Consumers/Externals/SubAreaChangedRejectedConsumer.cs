using Convey.CQRS.Events;
using CountwareTraffic.Services.Areas.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Api
{
    public class SubAreaChangedRejectedConsumer : IConsumer<Mhd.Framework.QueueModel.SubAreaChangedRejected>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public SubAreaChangedRejectedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SubAreaChangedRejected queuEvent)
        {
            var subAreaChangedRejected = _eventMapper.Map(queuEvent) as SubAreaChangedRejected;
            await _eventDispatcher.PublishAsync(subAreaChangedRejected);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
