using Convey.CQRS.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Application
{
    public class SubAreaChangedSuccessfullyHandler : ICommandHandler<SubAreaChangedSuccessfully>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<SubAreaChangedSuccessfullyHandler> _logger;
        public SubAreaChangedSuccessfullyHandler(IHubContext<NotificationsHub> hubContext, ILogger<SubAreaChangedSuccessfullyHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task HandleAsync(SubAreaChangedSuccessfully command)
        {
            await _hubContext.Clients
              .Group(command.UserName)
              .SendAsync("SubAreaChangedSuccessfully", new { SubAreaId = command.SubAreaId, OldName = command.OldName, NewName = command.NewName });
        }
    }
}
