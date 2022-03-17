using System;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class GetSubAreaDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AreaId { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public DateTime AuditModifiedDate { get; set; }
        public Guid AuditCreateBy { get; set; }
        public Guid AuditModifiedBy { get; set; }
    }
}
