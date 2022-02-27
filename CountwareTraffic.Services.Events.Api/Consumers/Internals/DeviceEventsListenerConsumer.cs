using Convey.CQRS.Commands;
using CountwareTraffic.Services.Events.Application;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api
{
    public class DeviceEventsListenerConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceEventsListener>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public DeviceEventsListenerConsumer(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceEventsListener queuEvent)
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
