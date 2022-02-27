using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub.Application
{
    [Authorize]
    public class NotificationsHub : Hub
    {
        private readonly IdentityService _identityService;
        public NotificationsHub(IdentityService identityService)
        {
            _identityService = identityService;
        }
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _identityService.UserId.ToString());
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _identityService.UserId.ToString());
            await base.OnDisconnectedAsync(ex);
        }
    }
}