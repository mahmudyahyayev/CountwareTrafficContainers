using Convey.CQRS.Commands;
using CountwareTraffic.Services.Events.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api
{
    public class DeviceEventsListenerConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceEventsListener>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public DeviceEventsListenerConsumer(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceEventsListener queuEvent)
        {
            CreateEvent createEvent = new()
            {
                Description = queuEvent.Description,
                DeviceId = queuEvent.DeviceId,
                DirectionTypeId = queuEvent.DirectionTypeId,
                EventDate = queuEvent.EventDate
            };

            await _commandDispatcher.SendAsync(createEvent);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
