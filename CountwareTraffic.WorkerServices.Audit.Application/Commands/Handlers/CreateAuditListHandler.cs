using Convey.CQRS.Commands;
using CountwareTraffic.WorkerServices.Audit.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Audit.Application
{
    public class CreateAuditListHandler : ICommandHandler<CreateAuditList>
    {
        private readonly IAuditLogMongoDbRepository _auditMongoDbRepository;
        private readonly ILogger<CreateAuditListHandler> _logger;
        public CreateAuditListHandler(IAuditLogMongoDbRepository auditMongoDbRepository, ILogger<CreateAuditListHandler> logger)
        {
            _auditMongoDbRepository = auditMongoDbRepository;
            _logger = logger;
        }
        public async Task HandleAsync(CreateAuditList command)
        {
            try
            {
                var audits = command.Items.Select(u => new AuditLog
                {
                    Actions = u.Actions,
                    ChangedColumns = u.ChangedColumns,
                    NewData = u.NewData,
                    OldData = u.OldData,
                    RecordId = u.RecordId,
                    RevisionStamp = u.RevisionStamp,
                    TableName = u.TableName,
                    UserId = u.UserId
                });

                await _auditMongoDbRepository.AddRangeAsync(audits.ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Insert audit log in mongodb error: {{ex.Message}}");
                throw new MongoDbInsertException(ex.Message);
            }
        }
    }
}
