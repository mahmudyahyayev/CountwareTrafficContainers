using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Workers.Audit.Application
{
    [Contract]
    public class CreateAuditList : ICommand
    {
        public CreateAuditList() => Items = new List<CreateAudit>();
        public List<CreateAudit> Items { get; set; }
    }

    public class CreateAudit
    {
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
