using Convey.CQRS.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Application
{
    public class DeviceChangedSuccessfullyHandler : ICommandHandler<DeviceChangedSuccessfully>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<DeviceChangedSuccessfullyHandler> _logger;
        public DeviceChangedSuccessfullyHandler(IHubContext<NotificationsHub> hubContext, ILogger<DeviceChangedSuccessfullyHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task HandleAsync(DeviceChangedSuccessfully command)
        {
            await _hubContext.Clients
              .Group(command.UserName)
              .SendAsync("DeviceChangedSuccessfully", new { DeviceId = command.DeviceId, OldName = command.OldName, NewName = command.NewName });
        }
    }
}
