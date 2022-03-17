using System.Threading.Tasks;
using Convey.CQRS.Commands;
using CountwareTraffic.Services.Areas.Infrastructure;
using Quartz;

namespace CountwareTraffic.Services.Areas.Outbox.Scheduler
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
