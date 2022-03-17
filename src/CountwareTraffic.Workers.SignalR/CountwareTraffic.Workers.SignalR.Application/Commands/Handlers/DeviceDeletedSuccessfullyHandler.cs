using Convey.CQRS.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Application
{
    public class DeviceDeletedSuccessfullyHandler : ICommandHandler<DeviceDeletedSuccessfully>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<DeviceDeletedSuccessfullyHandler> _logger;
        public DeviceDeletedSuccessfullyHandler(IHubContext<NotificationsHub> hubContext, ILogger<DeviceDeletedSuccessfullyHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task HandleAsync(DeviceDeletedSuccessfully command)
        {
            await _hubContext.Clients
              .Group(command.UserName)
              .SendAsync("DeviceDeletedSuccessfully", new { DeviceId = command.DeviceId, IsDeleted = command.IsDeleted });
        }
    }
}
