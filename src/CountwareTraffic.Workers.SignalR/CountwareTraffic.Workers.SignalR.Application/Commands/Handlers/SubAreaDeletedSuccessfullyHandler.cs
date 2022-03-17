using Convey.CQRS.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Application
{
    public class SubAreaDeletedSuccessfullyHandler : ICommandHandler<SubAreaDeletedSuccessfully>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<SubAreaDeletedSuccessfullyHandler> _logger;
        public SubAreaDeletedSuccessfullyHandler(IHubContext<NotificationsHub> hubContext, ILogger<SubAreaDeletedSuccessfullyHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task HandleAsync(SubAreaDeletedSuccessfully command)
        {
            await _hubContext.Clients
              .Group(command.UserName)
              .SendAsync("SubAreaDeletedSuccessfully", new { SubAreaId = command.SubAreaId, IsDeleted = command.IsDeleted });
        }
    }
}
