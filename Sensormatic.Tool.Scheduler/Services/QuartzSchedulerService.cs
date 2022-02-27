using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensormatic.Tool.Scheduler
{
    public class QuartzSchedulerService : IQuartzSchedulerService
    {
        private readonly ISchedulerFactory _schedulerFactory;

        public QuartzSchedulerService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public async Task<List<ScheduledJob>> GetJobsAsync()
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var result = new List<ScheduledJob>();

            var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());

            if (jobKeys == null || jobKeys.Count == 0)
                return result;

            var currentlyExecutingJobs = await scheduler.GetCurrentlyExecutingJobs();

            var triggerKeys = await scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());

            if (triggerKeys == null || triggerKeys.Count == 0)
                return result;

            foreach (var triggerKey in triggerKeys)
            {
                if (triggerKey.Group == "DEFAULT")
                    continue;

                var trigger = await scheduler.GetTrigger(triggerKey);
                var job = await scheduler.GetJobDetail(trigger.JobKey);

                var nextFireTime = trigger.GetNextFireTimeUtc();
                var previousFireTime = trigger.GetPreviousFireTimeUtc();
                var executingJob = currentlyExecutingJobs
                    .Where(x => x.JobDetail.Key.Name == trigger.JobKey.Name && x.JobDetail.Key.Group == trigger.JobKey.Group)
                    .FirstOrDefault();

                var scheduledJob = new ScheduledJob
                {
                    JobName = job.Key.Name,
                    Description = job.Description,
                    NextFireTime = nextFireTime.HasValue ? nextFireTime.Value.LocalDateTime.ToString("dd.MM.yyyy HH:mm:ss") : "",
                    PreviousFireTime = previousFireTime.HasValue ? previousFireTime.Value.LocalDateTime.ToString("dd.MM.yyyy HH:mm:ss") : "",
                    FireTime = executingJob == null ? "" : executingJob.FireTimeUtc.LocalDateTime.ToString("dd.MM.yyyy HH:mm:ss"),
                    TriggerDescription = trigger.Description,
                    TriggerState = (await scheduler.GetTriggerState(trigger.Key)).ToString()
                };

                result.Add(scheduledJob);
            }

            return result;
        }

        public JobExecutedResponse GetJobExecutionInfo(IJobExecutionContext context)
        {
            var result = new JobExecutedResponse
            {
                JobName = context.JobDetail.Key.Name,
                LastRunTime = new DateTime(context.JobRunTime.Ticks).ToString("HH:mm:ss"),
                TriggerState = context.Scheduler.GetTriggerState(context.Trigger.Key).ToString()
            };

            return result;
        }

        public JobExecutingResponse GetJobExecutingInfo(IJobExecutionContext context)
        {
            var nextFireTime = context.Trigger.GetNextFireTimeUtc();
            var previousFireTime = context.Trigger.GetPreviousFireTimeUtc();

            var result = new JobExecutingResponse
            {
                JobName = context.JobDetail.Key.Name,
                NextFireTime = nextFireTime.HasValue ? nextFireTime.Value.LocalDateTime.ToString("dd.MM.yyyy HH:mm:ss") : "",
                PreviousFireTime = previousFireTime.HasValue ? previousFireTime.Value.LocalDateTime.ToString("dd.MM.yyyy HH:mm:ss") : "",
                FireTime = context.FireTimeUtc.LocalDateTime.ToString("dd.MM.yyyy HH:mm:ss")
            };

            return result;
        }

        public async Task TriggerJobAsync(string jobName)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var keys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());

            var key = keys.FirstOrDefault(x => x.Name == jobName);

            var triggers = await scheduler.GetTriggersOfJob(key);

            var triggerKey = triggers.FirstOrDefault();

            if (triggerKey != null && await scheduler.GetTriggerState(triggerKey.Key) == TriggerState.Paused)
                await scheduler.ResumeJob(key);
            else
                await scheduler.TriggerJob(key);
        }

        public async Task PauseJobAsync(string jobName)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var keys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());

            var key = keys.FirstOrDefault(x => x.Name == jobName);

            await scheduler.PauseJob(key);
        }

        public async Task PauseJobsAsync()
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            await scheduler.PauseJobs(GroupMatcher<JobKey>.AnyGroup());
        }

        public async Task ResumeJobsAsync()
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            await scheduler.ResumeJobs(GroupMatcher<JobKey>.AnyGroup());
        }

        public async Task ResumeJobAsync(string jobName)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var keys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());

            var key = keys.FirstOrDefault(x => x.Name == jobName);

            await scheduler.ResumeJob(key);
        }
    }
}
