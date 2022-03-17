using MongoDB.Bson;
using Mhd.Framework.MongoDb;

namespace CountwareTraffic.Workers.Sms.Data
{
    public class SmsTemplate : MongoDbEntity
    {
        public SmsTemplate()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public string Type { get; set; }
        public string Description { get; set; }
        public string Template { get; set; }
        public bool IsOtp { get; set; }
    }
}
