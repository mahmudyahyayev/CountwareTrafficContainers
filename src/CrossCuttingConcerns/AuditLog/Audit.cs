using System;

namespace Countware.Traffic.AuditLog
{
    public class Audit
    {
        public string RecordId { get; set; }
        public DateTime? RevisionStamp { get; set; }
        public string TableName { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public string Actions { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public string ChangedColumns { get; set; }
    }
}
