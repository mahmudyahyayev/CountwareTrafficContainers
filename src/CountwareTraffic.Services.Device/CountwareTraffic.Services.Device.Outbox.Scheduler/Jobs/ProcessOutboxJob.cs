using Convey.CQRS.Commands;
using CountwareTraffic.Services.Devices.Infrastructure;
using Quartz;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Outbox.Scheduler
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxJob : IJob
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public ProcessOutboxJob(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _commandDispatcher.SendAsync(new ProcessOutboxSchedule { });
        }
    }
}
