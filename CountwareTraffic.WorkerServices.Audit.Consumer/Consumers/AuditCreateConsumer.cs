using Convey.CQRS.Commands;
using CountwareTraffic.WorkerServices.Audit.Application;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.Queue;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace CountwareTraffic.WorkerServices.Audit.Consumer
{
    public class AuditCreateConsumer : IConsumer<Sensormatic.Tool.QueueModel.AuditList<Countware.Traffic.CrossCC.AuditLog.Audit>>, ITransientSelfDependency
    {
        private readonly ICommandDispatcher _commandDispatcher;
        
        public AuditCreateConsumer(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        public async Task ConsumeAsync(Sensormatic.Tool.QueueModel.AuditList<Countware.Traffic.CrossCC.AuditLog.Audit> message)
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
