using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.ElasticRepair
{
    public class JobListener : IJobListener
    {
        public string Name => throw new System.NotImplementedException();

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
