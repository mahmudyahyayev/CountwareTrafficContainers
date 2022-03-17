using Convey.CQRS.Commands;
using CountwareTraffic.Workers.Audit.Application;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace CountwareTraffic.Workers.Audit.Consumer
{
    public class AuditCreateConsumer : IConsumer<Mhd.Framework.QueueModel.AuditList<Countware.Traffic.AuditLog.Audit>>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;
        
        public AuditCreateConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.AuditList<Countware.Traffic.AuditLog.Audit> message)
        {
            CreateAuditList createAuditList = new();

            createAuditList.Items = message.Items.Select(u => new CreateAudit
            {
                Actions = u.Actions,
                RecordId = u.RecordId,
                ChangedColumns = u.ChangedColumns,
                NewData = u.NewData,
                OldData = u.OldData,
                RevisionStamp = u.RevisionStamp,
                TableName = u.TableName,
                UserId = u.UserId
            }).ToList();

            await _commandDispatcher.SendAsync(createAuditList);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
