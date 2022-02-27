using Microsoft.AspNetCore.SignalR;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sensormatic.Tool.Scheduler
{
    public class SchedulerListener : ISchedulerListener
    {
        private readonly IHubContext<JobHub> _hubContext;

        public SchedulerListener(IHubContext<JobHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("jobAdded", jobDetail.Key.Name);
            }
            catch
            {
            }
        }

        public async Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("jobDeleted", jobKey.Name);
            }
            catch
            {
            }
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("jobPaused", jobKey.Name);
            }
            catch
            {
            }
        }

        public async Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("jobResumed", jobKey.Name);
            }
            catch
            {
            }
        }

        public async Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("jobScheduled", trigger.JobKey.Name);
            }
            catch
            {
            }
        }

        public async Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("jobsPaused", "All jobs paused!");
            }
            catch
            {
            }
        }

        public async Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("jobsResumed", "All jobs resumed!");
            }
            catch
            {
            }
        }

        public async Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
        }

        public async Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
        }

        public async Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
        }

        public async Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
        }

        public async Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
        }

        public async Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
        }

        public async Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
        }

        public async Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
        }

        public async Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
        }

        public async Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
        }

        public async Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
        }

        public async Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
        }

        public async Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
        }
    }
}
