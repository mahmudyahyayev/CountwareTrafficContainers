using Convey.CQRS.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Application
{
    public class DeviceDeletedFailedHandler : ICommandHandler<DeviceDeletedFailed>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<DeviceDeletedFailedHandler> _logger;
        public DeviceDeletedFailedHandler(IHubContext<NotificationsHub> hubContext, ILogger<DeviceDeletedFailedHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task HandleAsync(DeviceDeletedFailed command)
        {
            await _hubContext.Clients
              .Group(command.UserName)
              .SendAsync("DeviceDeletedFailed", new { DeviceId = command.DeviceId, IsDeleted = command.IsDeleted });
        }
    }
}
