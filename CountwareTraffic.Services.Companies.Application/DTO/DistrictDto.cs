using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class DistrictDto
    {
        public Guid Id { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public DateTime AuditModifiedDate { get; set; }
        public Guid AuditCreateBy { get; set; }
        public Guid AuditModifiedBy { get; set; }
    }
}
