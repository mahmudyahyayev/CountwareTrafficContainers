using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading;
using System.Threading.Tasks;

namespace Mhd.Framework.Scheduler
{
    public class TriggerListener : ITriggerListener
    {
        private readonly ILogger _logger;
        private readonly IHubContext<JobHub> _hubContext;

        public TriggerListener(ILoggerFactory loggerFactory, IHubContext<JobHub> hubContext)
        {
            _logger = loggerFactory.CreateLogger<TriggerListener>();
            _hubContext = hubContext;
        }

        public string Name => "TriggerListener";

        public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public async Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{TriggerKey} misfired at {PreviousFireTimeUtc}.Next: {NextFireTimeUtc}", trigger.Key, trigger.GetPreviousFireTimeUtc(), trigger.GetNextFireTimeUtc());

            await _hubContext.Clients.All.SendAsync("triggerMisfired", $"{trigger.Key} misfired at {trigger.GetPreviousFireTimeUtc()}");
        }

        public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(false);
        }
    }
}
