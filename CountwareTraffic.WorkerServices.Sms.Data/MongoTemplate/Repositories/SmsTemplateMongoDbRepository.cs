using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.MongoDb;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Sms.Data
{
    public interface ISmsTemplateMongoDbRepository : ISingletonDependency
    {
        Task<TemplatedSmsDTO> GetAsync(string type);
        Task AddAsync(SmsTemplate smsTemplate); //denemdir
    }

    public class SmsTemplateMongoDbRepository : MongoDbRepository<SmsTemplate>, ISmsTemplateMongoDbRepository
    {
        public SmsTemplateMongoDbRepository(IConfiguration configuration) : base(configuration, "Documents", "SmsTemplates") { }

        public async new Task AddAsync(SmsTemplate smsTemplate)
        {
            await base.AddAsync(smsTemplate);
        }

        public async Task<TemplatedSmsDTO> GetAsync(string type)
        {
            var template = await Collection.Find(x => x.Type == type)
                .Project(x => new TemplatedSmsDTO { Template = x.Template, IsOtp = x.IsOtp })
                .FirstOrDefaultAsync();

            return template;
        }
    }
}
