using Microsoft.AspNetCore.SignalR;
using Quartz;
using System.Threading.Tasks;

namespace Sensormatic.Tool.Scheduler
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

