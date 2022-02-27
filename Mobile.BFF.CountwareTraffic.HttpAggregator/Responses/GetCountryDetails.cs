using System;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class GetCountryDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public DateTime AuditModifiedDate { get; set; }
        public Guid AuditCreateBy { get; set; }
        public Guid AuditModifiedBy { get; set; }
        public string Iso { get; set; }
        public string Iso3 { get; set; }
        public int? IsoNumeric { get; set; }
        public string Capital { get; set; }
        public string ContinentCode { get; set; }
        public string CurrencyCode { get; set; }
    }
}
