using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Api
{
    public class DeviceCreatedRejectedConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceCreatedRejected>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public DeviceCreatedRejectedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceCreatedRejected queuEvent)
        {
            var deviceCreatedRejected = _eventMapper.Map(queuEvent) as DeviceCreatedRejected;
            await _eventDispatcher.PublishAsync(deviceCreatedRejected);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
