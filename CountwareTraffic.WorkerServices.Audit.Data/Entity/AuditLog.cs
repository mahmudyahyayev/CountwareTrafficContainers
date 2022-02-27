using MongoDB.Bson;
using Sensormatic.Tool.MongoDb;
using System;

namespace CountwareTraffic.WorkerServices.Audit.Data
{
    public class AuditLog : MongoDbEntity
    {
        public AuditLog() => Id = ObjectId.GenerateNewId().ToString();
        public string RecordId { get; set; }
        public DateTime? RevisionStamp { get; set; }
        public string TableName { get; set; }
        public Guid UserId { get; set; }
        public string Actions { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public string ChangedColumns { get; set; }
    }
}
