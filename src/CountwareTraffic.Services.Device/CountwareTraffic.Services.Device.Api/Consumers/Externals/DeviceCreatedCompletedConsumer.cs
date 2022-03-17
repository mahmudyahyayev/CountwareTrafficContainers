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
    public class DeviceCreatedCompletedConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceCreatedCompleted>, ITransientSelfDependency
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        public DeviceCreatedCompletedConsumer(IEventDispatcher eventDispatcher, IEventMapper eventMapper)
        {
            _eventDispatcher = eventDispatcher;
            _eventMapper = eventMapper;
        }
        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceCreatedCompleted queuEvent)
        {
            var deviceCreatedCompleted = _eventMapper.Map(queuEvent) as DeviceCreatedCompleted;
            await _eventDispatcher.PublishAsync(deviceCreatedCompleted);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
