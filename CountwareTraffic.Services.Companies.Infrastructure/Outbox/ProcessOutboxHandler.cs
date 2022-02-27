using Convey.CQRS.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class ProcessOutboxHandler : ICommandHandler<ProcessOutbox>
    {
        private readonly AreaDbContext _context;
        public ProcessOutboxHandler(AreaDbContext context)
        {
            _context = context;
        }
        public async Task HandleAsync(ProcessOutbox command)
        {
            //var connection = _context.Database.GetDbConnection();

            //const string sql = @"SELECT 
            //                        [OutboxMessage].[Id], 
            //                        [OutboxMessage].[Type],
            //                        [OutboxMessage].[Data] 
            //                        FROM [area.app].[OutboxMessages] AS [OutboxMessage] 
            //                             WHERE [OutboxMessage].[EventRecordId] = @EventRecordId";

            //var message = await connection.QueryFirstOrDefaultAsync<OutboxMessageDto>(sql, new { EventRecordId = command.RecordId });

            //if (message != null)
            //{
            //    const string sqlUpdate = @"UPDATE [area.app].[OutboxMessages]
            //                                    SET [ProcessedDate] = @Date,
            //                                        [LastException] = @Exception,
            //                                        [IsTryFromQueue] = 1
            //                                    WHERE [Id] = @Id";

            //    var result = await connection.ExecuteAsync(sqlUpdate, new { @Date = DateTime.Now, LastException = command.ExceptionMessage });
            //}

            try
            {
                var outboxMessage = await _context.Set<OutboxMessage>().AsQueryable().SingleOrDefaultAsync(u => u.EventRecordId == command.RecordId);

                outboxMessage.IsTryFromQueue = true;
                outboxMessage.ProcessedDate = DateTime.Now;
                outboxMessage.LastException = command.ExceptionMessage;

                await _context.SaveChangesAsync();
            }
            catch (Exception ) {}
        }
    }
}
