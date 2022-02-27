using Quartz;
using Sensormatic.Tool.Ioc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sensormatic.Tool.Scheduler
{
    public interface IQuartzSchedulerService : ISingletonDependency
    {
        Task<List<ScheduledJob>> GetJobsAsync();
        JobExecutedResponse GetJobExecutionInfo(IJobExecutionContext context);
        JobExecutingResponse GetJobExecutingInfo(IJobExecutionContext context);
        Task TriggerJobAsync(string jobName);
        Task PauseJobAsync(string jobName);
        Task ResumeJobAsync(string jobName);
        Task PauseJobsAsync();
        Task ResumeJobsAsync();
    }
}
