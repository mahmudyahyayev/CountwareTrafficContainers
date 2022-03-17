using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Api
{
    public class DeviceStatusChangedConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceStatusChanged>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public DeviceStatusChangedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceStatusChanged queuEvent)
        {
            var deviceStatusChanged = _eventMapper.Map(queuEvent) as DeviceStatusChanged;
            await _eventDispatcher.PublishAsync(deviceStatusChanged);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
