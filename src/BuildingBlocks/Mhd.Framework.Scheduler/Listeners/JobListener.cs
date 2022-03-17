using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading;
using System.Threading.Tasks;

namespace Mhd.Framework.Scheduler
{
    public class JobListener : IJobListener
    {
        private readonly ILogger _logger;
        private readonly IHubContext<JobHub> _hubContext;
        private readonly IQuartzSchedulerService _schedulerService;

        public JobListener(ILoggerFactory loggerFactory, IHubContext<JobHub> hubContext, IQuartzSchedulerService schedulerService)
        {
            _logger = loggerFactory.CreateLogger<JobListener>();
            _hubContext = hubContext;
            _schedulerService = schedulerService;
        }

        public string Name => "JobListener";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{JobName} Execution Vetoed", context.JobDetail.JobType.Name);

            return Task.CompletedTask;
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{JobName} To Be Executed", context.JobDetail.JobType.Name);

            try
            {
                await _hubContext.Clients.All.SendAsync("jobExecuting", _schedulerService.GetJobExecutingInfo(context));
            }
            catch
            {
            }
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{JobName} Was Executed in {JobRunTime}", context.JobDetail.JobType.Name, context.JobRunTime);

            try
            {
                await _hubContext.Clients.All.SendAsync("jobExecuted", _schedulerService.GetJobExecutionInfo(context));
            }
            catch
            {
            }
        }
    }
}
