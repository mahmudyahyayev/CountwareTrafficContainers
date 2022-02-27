using Convey.CQRS.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Application
{
    public class DeviceCreatedFailedHandler : ICommandHandler<DeviceCreatedFailed>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<DeviceCreatedFailedHandler> _logger;
        public DeviceCreatedFailedHandler(IHubContext<NotificationsHub> hubContext, ILogger<DeviceCreatedFailedHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task HandleAsync(DeviceCreatedFailed command)
        {
            await _hubContext.Clients
              .Group(command.UserName)
              .SendAsync("DeviceCreatedFailed", new { DeviceId = command.DeviceId, DeviceCreationStatus = command.DeviceCreationStatus });
        }
    }

}
