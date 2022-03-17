using System;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class GetDistrictDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public DateTime AuditModifiedDate { get; set; }
        public Guid AuditCreateBy { get; set; }
        public Guid AuditModifiedBy { get; set; }
    }
}
