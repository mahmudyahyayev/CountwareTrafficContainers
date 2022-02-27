using Microsoft.Extensions.Configuration;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.MongoDb;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Audit.Data
{
    public interface IAuditLogMongoDbRepository : ISingletonDependency
    {
        Task AddRangeAsync(params AuditLog[] audit); 
    }

    public class SmsTemplateMongoDbRepository : MongoDbRepository<AuditLog>, IAuditLogMongoDbRepository
    {
        public SmsTemplateMongoDbRepository(IConfiguration configuration) : base(configuration, "Logs", "Audits") { }

        public async new Task AddRangeAsync(AuditLog[] audit) => await base.AddRangeAsync(audit);
    }
}
