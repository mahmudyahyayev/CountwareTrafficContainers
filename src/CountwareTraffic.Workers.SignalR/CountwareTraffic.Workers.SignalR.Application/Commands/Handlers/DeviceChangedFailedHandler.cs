using Convey.CQRS.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Application
{
    public class DeviceChangedFailedHandler : ICommandHandler<DeviceChangedFailed>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<DeviceChangedFailedHandler> _logger;
        public DeviceChangedFailedHandler(IHubContext<NotificationsHub> hubContext, ILogger<DeviceChangedFailedHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task HandleAsync(DeviceChangedFailed command)
        {
            await _hubContext.Clients
              .Group(command.UserName)
              .SendAsync("DeviceChangedFailed", new { DeviceId = command.DeviceId, OldName = command.OldName, NewName = command.NewName });
        }
    }
}
