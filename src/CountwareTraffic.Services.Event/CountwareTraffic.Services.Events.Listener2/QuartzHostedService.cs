using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Listener2
{
    public class QuartzHostedService : BackgroundService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;
        private readonly ILogger<QuartzHostedService> _logger;

        public QuartzHostedService(ILogger<QuartzHostedService> logger, ISchedulerFactory schedulerFactory, IEnumerable<JobSchedule> jobSchedules, IJobFactory jobFactory)
        {
            _schedulerFactory = schedulerFactory;
            _jobSchedules = jobSchedules;
            _jobFactory = jobFactory;
            _logger = logger;
        }

        public IScheduler Scheduler { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(stoppingToken);
            Scheduler.JobFactory = _jobFactory;

            foreach (var jobSchedule in _jobSchedules)
            {
                var job = CreateJob(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);
                await Scheduler.ScheduleJob(job, trigger, stoppingToken);
            }
            await Scheduler.Start(stoppingToken); 
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("QuartzHostedService forcing to stop...");

            await Scheduler?.Shutdown(cancellationToken);

            await Task.Delay(5000);

            await base.StopAsync(cancellationToken);
        }

        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            return TriggerBuilder
                .Create()
                .StartNow()
                .WithIdentity($"{schedule.JobName}.trigger", $"{schedule.JobName}.trigger.group")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription("1 time in 10 seconds")
                .ForJob(schedule.JobName, $"{schedule.JobName}.group")
                .WithSimpleSchedule(x => x.RepeatForever().WithIntervalInSeconds(10))
                .Build();
        }

        private static IJobDetail CreateJob(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(schedule.JobName, $"{schedule.JobName}.group")
                .WithDescription("Pull the events from device and insert db")
                .Build();
        }
    }
}