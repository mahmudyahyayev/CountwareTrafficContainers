using Quartz;
using Quartz.Spi;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Listener.Scheduler
{
    public class AutoDeleteDeviceEventsJobConsumer : IConsumer<Sensormatic.Tool.QueueModel.DeviceDeletedSuccessfully>, ITransientSelfDependency
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        public AutoDeleteDeviceEventsJobConsumer(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
        }

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.DeviceDeletedSuccessfully queuEvent)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            scheduler.JobFactory = _jobFactory;

            var jobKey = new JobKey(queuEvent.DeviceName, $"{queuEvent.DeviceName}.group");

            await scheduler.DeleteJob(jobKey);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
