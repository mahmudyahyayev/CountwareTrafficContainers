using Convey.CQRS.Events;
using CountwareTraffic.Services.Companies.Application;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Api
{
    public class SubAreaCreatedCompletedConsumer : IConsumer<Sensormatic.Tool.QueueModel.SubAreaCreatedCompleted>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public SubAreaCreatedCompletedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.SubAreaCreatedCompleted queuEvent)
        {
            var subAreaCreatedCompleted = _eventMapper.Map(queuEvent) as SubAreaCreatedCompleted;
            await _eventDispatcher.PublishAsync(subAreaCreatedCompleted);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
