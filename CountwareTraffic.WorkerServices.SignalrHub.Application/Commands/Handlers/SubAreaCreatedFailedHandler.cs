using Convey.CQRS.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Application
{
    public class SubAreaCreatedFailedHandler : ICommandHandler<SubAreaCreatedFailed>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<SubAreaCreatedFailedHandler> _logger;
        public SubAreaCreatedFailedHandler(IHubContext<NotificationsHub> hubContext, ILogger<SubAreaCreatedFailedHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task HandleAsync(SubAreaCreatedFailed command)
        {
            await _hubContext.Clients
              .Group(command.UserName)
              .SendAsync("SubAreaCreatedFailed", new { SubAreaId = command.SubAreaId, SubAreaStatus = command.SubAreaStatus});
        }
    }
}
