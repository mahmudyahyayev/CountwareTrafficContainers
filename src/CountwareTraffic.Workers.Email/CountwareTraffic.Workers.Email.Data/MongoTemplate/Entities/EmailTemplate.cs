using MongoDB.Bson;
using Mhd.Framework.MongoDb;
using System.Collections.Generic;

namespace CountwareTraffic.Workers.Email.Data
{
    public class EmailTemplate : MongoDbEntity
    {
        public EmailTemplate() => Id = ObjectId.GenerateNewId().ToString();

        public string Type { get; set; }
        public string Description { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
        public bool IsHtml { get; set; }
        public string From { get; set; }
        public IList<string> To { get; set; }
        public IList<string> Cc { get; set; }
        public IList<string> Bc { get; set; }
    }
}
