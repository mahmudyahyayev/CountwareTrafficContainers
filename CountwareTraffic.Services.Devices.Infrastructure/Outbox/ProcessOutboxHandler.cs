using Convey.CQRS.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class ProcessOutboxHandler : ICommandHandler<ProcessOutbox>
    {
        private readonly DeviceDbContext _context;
        public ProcessOutboxHandler(DeviceDbContext context)
        {
            _context = context;
        }
        public async Task HandleAsync(ProcessOutbox command)
        {
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
