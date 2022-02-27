using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Application;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Api
{
    public class DeviceChangedRejectedConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceChangedRejected>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public DeviceChangedRejectedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceChangedRejected queuEvent)
        {
            var deviceChangedRejected = _eventMapper.Map(queuEvent) as DeviceChangedRejected;
            await _eventDispatcher.PublishAsync(deviceChangedRejected);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
