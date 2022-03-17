using Convey.CQRS.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Application
{
    public class SubAreaChangedFailedHandler : ICommandHandler<SubAreaChangedFailed>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<SubAreaChangedFailedHandler> _logger;
        public SubAreaChangedFailedHandler(IHubContext<NotificationsHub> hubContext, ILogger<SubAreaChangedFailedHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task HandleAsync(SubAreaChangedFailed command)
        {
            await _hubContext.Clients
              .Group(command.UserName)
              .SendAsync("SubAreaChangedFailed", new { SubAreaId = command.SubAreaId, OldName = command.OldName, NewName = command.NewName });
        }
    }
}
