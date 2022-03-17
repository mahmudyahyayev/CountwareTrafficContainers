using Convey.CQRS.Events;
using CountwareTraffic.Services.Areas.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Api
{
    public class SubAreaCreatedCompletedConsumer : IConsumer<Mhd.Framework.QueueModel.SubAreaCreatedCompleted>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public SubAreaCreatedCompletedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Mhd.Framework.QueueModel.SubAreaCreatedCompleted queuEvent)
        {
            var subAreaCreatedCompleted = _eventMapper.Map(queuEvent) as SubAreaCreatedCompleted;
            await _eventDispatcher.PublishAsync(subAreaCreatedCompleted);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
