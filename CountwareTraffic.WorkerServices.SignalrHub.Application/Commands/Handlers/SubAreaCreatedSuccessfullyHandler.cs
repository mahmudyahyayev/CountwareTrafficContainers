using Convey.CQRS.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Application
{
    public class SubAreaCreatedSuccessfullyHandler : ICommandHandler<SubAreaCreatedSuccessfully>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<SubAreaCreatedSuccessfullyHandler> _logger;
        public SubAreaCreatedSuccessfullyHandler(IHubContext<NotificationsHub> hubContext, ILogger<SubAreaCreatedSuccessfullyHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task HandleAsync(SubAreaCreatedSuccessfully command)
        {
            await _hubContext.Clients
              .Group(command.UserName)
              .SendAsync("SubAreaCreatedSuccessfully", new {SubAreaId = command.SubAreaId, Status = command.SubAreaStatus});
        }
    }
}
