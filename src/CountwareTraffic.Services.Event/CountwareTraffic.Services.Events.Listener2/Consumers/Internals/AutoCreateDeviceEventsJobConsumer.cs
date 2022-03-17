using Quartz;
using Quartz.Spi;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Listener2
{
    public class AutoCreateDeviceEventsJobConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceCreatedCompleted>, ITransientSelfDependency
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        public AutoCreateDeviceEventsJobConsumer(ISchedulerFactory schedulerFactory,  IJobFactory jobFactory)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
        }

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceCreatedCompleted queuEvent)
        {
            if (queuEvent.Name == null) return;

            var scheduler = await _schedulerFactory.GetScheduler();

            scheduler.JobFactory = _jobFactory;

            var jobSchedule = new JobSchedule(jobType: typeof(DeviceEventsListenerJob), cronExpression: "0/10 * * * * ?", jobName: queuEvent.Name);

            var job = CreateJob(jobSchedule);

            var trigger = CreateTrigger(jobSchedule);

            await scheduler.ScheduleJob(job, trigger);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);

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
