using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Api
{
    public class DeviceChangedRejectedConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceChangedRejected>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public DeviceChangedRejectedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceChangedRejected queuEvent)
        {
            var deviceChangedRejected = _eventMapper.Map(queuEvent) as DeviceChangedRejected;
            await _eventDispatcher.PublishAsync(deviceChangedRejected);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
