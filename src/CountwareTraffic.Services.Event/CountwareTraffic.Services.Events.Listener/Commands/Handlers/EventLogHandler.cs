using Convey.CQRS.Commands;
using CountwareTraffic.Services.Events.Application;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Listener
{
    public class EventLogHandler : ICommandHandler<EventLog>
    {
        private readonly IIdentityService _identityService;
        private readonly ICommandDispatcher _commandDispatcher;
        public EventLogHandler(IIdentityService identityService, ICommandDispatcher commandDispatcher)
        {
            _identityService = identityService;
            _commandDispatcher = commandDispatcher;
        }

        public async Task HandleAsync(EventLog eventLog)
        {
            var userId = _identityService.UserId;

            DeviceEventsListener deviceEventsListener = new()
            {
                Description = "Iceriye dogru giris yapildi",
                DeviceId = eventLog.DeviceId,
                UserId = userId,
                RecordId = Guid.NewGuid(),
                EventDate = DateTime.Now,
                DeviceName = eventLog.DeviceName,
                DirectionTypeId = eventLog.DirectionTypeId
            };

            await _commandDispatcher.SendAsync(deviceEventsListener);
        }
    }
}
