using Quartz;
using Quartz.Spi;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Listener2
{
    public class AutoDeleteDeviceEventsJobConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceDeletedSuccessfully>, ITransientSelfDependency
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        public AutoDeleteDeviceEventsJobConsumer(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
        }

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceDeletedSuccessfully queuEvent)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            scheduler.JobFactory = _jobFactory;

            var jobKey = new JobKey(queuEvent.DeviceName, $"{queuEvent.DeviceName}.group");

            await scheduler.DeleteJob(jobKey);
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
