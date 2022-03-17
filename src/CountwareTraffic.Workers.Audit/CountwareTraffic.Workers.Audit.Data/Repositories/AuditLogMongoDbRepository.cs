using Microsoft.Extensions.Configuration;
using Mhd.Framework.Ioc;
using Mhd.Framework.MongoDb;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Audit.Data
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
