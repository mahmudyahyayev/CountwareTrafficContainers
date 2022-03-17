using Convey.CQRS.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Application
{
    public class  SubAreaDeletedFailedHandler : ICommandHandler<SubAreaDeletedFailed>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<SubAreaDeletedFailedHandler> _logger;
        public SubAreaDeletedFailedHandler(IHubContext<NotificationsHub> hubContext, ILogger<SubAreaDeletedFailedHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task HandleAsync(SubAreaDeletedFailed command)
        {
            await _hubContext.Clients
              .Group(command.UserName)
              .SendAsync("SubAreaDeletedFailed", new { SubAreaId = command.SubAreaId, IsDeleted = command.IsDeleted });
        }
    }
}