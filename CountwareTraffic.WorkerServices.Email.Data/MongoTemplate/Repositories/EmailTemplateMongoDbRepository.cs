using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Sensormatic.Tool.Ioc;
using Sensormatic.Tool.MongoDb;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Email.Data
{
    public interface IEmailTemplateMongoDbRepository : ISingletonDependency
    {
        Task<TemplatedEmailDTO> GetAsync(string type);
        Task AddAsync(EmailTemplate emailTemple); //denemdir
    }

    public class EmailTemplateMongoDbRepository : MongoDbRepository<EmailTemplate>, IEmailTemplateMongoDbRepository
    {
        public EmailTemplateMongoDbRepository(IConfiguration configuration) : base(configuration, "Documents", "EmailTemplates") { }

        public async new Task AddAsync(EmailTemplate emailTemple)
        {
            await base.AddAsync(emailTemple);
        }

        public async Task<TemplatedEmailDTO> GetAsync(string type)
        {
            var template = await Collection.Find(x => x.Type == type)
                 .Project(x => new TemplatedEmailDTO
                 {
                     Bc = x.Bc,
                     BodyTemplate = x.BodyTemplate,
                     Cc = x.Cc,
                     From = x.From,
                     IsHtml = x.IsHtml,
                     SubjectTemplate = x.SubjectTemplate,
                     To = x.To
                 })
                 .FirstOrDefaultAsync();

            return template;
        }
    }
}
