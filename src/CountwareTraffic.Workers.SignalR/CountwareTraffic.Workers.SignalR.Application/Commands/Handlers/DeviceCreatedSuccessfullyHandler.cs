using Convey.CQRS.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Application
{
    public class DeviceCreatedSuccessfullyHandler : ICommandHandler<DeviceCreatedSuccessfully>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<DeviceCreatedSuccessfullyHandler> _logger;
        public DeviceCreatedSuccessfullyHandler(IHubContext<NotificationsHub> hubContext, ILogger<DeviceCreatedSuccessfullyHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task HandleAsync(DeviceCreatedSuccessfully command)
        {
            await _hubContext.Clients
              .Group(command.UserName)
              .SendAsync("DeviceCreatedSuccessfully", new { DeviceId = command.DeviceId, Status = command.DeviceCreationStatus });
        }
    }
}
