using Microsoft.AspNetCore.SignalR;
using Quartz;
using System.Threading.Tasks;

namespace Mhd.Framework.Scheduler
{
    public class JobHub : Hub
    {
        public JobHub()
        {
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}

