using Convey.CQRS.Commands;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Mhd.Framework.Queue;
using Mhd.Framework.QueueModel;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class ProcessOutboxScheduleHandler : ICommandHandler<ProcessOutboxSchedule>
    {
        private readonly EventDbContext _context;
        private readonly IQueueService _queueService;
        private readonly ILogger<ProcessOutboxScheduleHandler> _logger;
        public ProcessOutboxScheduleHandler(EventDbContext context, IQueueService queueService, ILogger<ProcessOutboxScheduleHandler> logger)
        {
            _context = context;
            _queueService = queueService;
            _logger = logger;
        }

        public async Task HandleAsync(ProcessOutboxSchedule command)
        {
            var connection = _context.Database.GetDbConnection();
            const string sql = @"SELECT [OutboxMessage].[Id],
                                        [OutboxMessage].[Type],
	                                    [OutboxMessage].[Data]
                                   FROM [event.app].[OutboxMessages] AS [OutboxMessage] 
	                                    WHERE [IsTryFromQueue] = 1 AND [LastException] IS NOT NULL";

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
            var messagesList = messages.AsList();

            const string sqlUpdateProcessedDate = @"UPDATE [event.app].[OutboxMessages] SET [ProcessedDate] = @Date,  [LastException] = NULL WHERE [Id] = @Id";

            if (messagesList.Count > 0)
            {
                foreach (var message in messagesList)
                {
                    try
                    {
                        Type type = Assemblies.Application.GetType(message.Type);

                        var queueEvent = JsonConvert.DeserializeObject(message.Data, type) as IQueueEvent;

                        if (queueEvent != null)
                        {
                            _queueService.Publish(queueEvent);

                            await connection.ExecuteAsync(sqlUpdateProcessedDate, new
                            {
                                Date = DateTime.UtcNow,
                                message.Id
                            });

                            _logger.LogInformation($"ProcessOutboxSchedule worked again for {message.Id} record.");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"ProcessOutboxSchedule got error for {message.Id} record. Exception: {ex.Message}");
                    }
                }
            }
        }
    }
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(DeviceCreated).Assembly;
    }
}
