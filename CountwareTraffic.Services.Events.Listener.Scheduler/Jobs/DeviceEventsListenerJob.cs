using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using CountwareTraffic.Services.Events.Application;
using Quartz;
using System;
using System.Threading.Tasks;


namespace CountwareTraffic.Services.Events.Listener.Scheduler
{
    [DisallowConcurrentExecution]
    public class DeviceEventsListenerJob : IJob
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public DeviceEventsListenerJob(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            string deviceName = context.JobDetail.Key.Name;

            var deviceInfo = await _queryDispatcher.QueryAsync(new GetDevice { DeviceName = deviceName });

            if (deviceInfo == null) return;

            //Bu kisimda cihaz bilgilerine gore Cihaza baglan ve veri cek.

            DeviceEventsListener command = new()
            {
                Description = "Iceriye dogru giris yapildi",
                DeviceId = deviceInfo.Id,
                UserId = new Guid("9dbc40ad-fab2-44e2-1261-08d9ef9b76fa"),
                RecordId = Guid.NewGuid(),
                EventDate = DateTime.Now,
                DeviceName = deviceInfo.Name,
                DirectionTypeId = 1
            };

            await _commandDispatcher.SendAsync(command);
        }
    }
}
