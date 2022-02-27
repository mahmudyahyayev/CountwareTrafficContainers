using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class CountryDetailsDto : CountryDto
    { 
        public string Iso { get; set; }
        public string Iso3 { get; set; }
        public int IsoNumeric { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string ContinentCode { get; set; }
        public string CurrencyCode { get; set; }
        public Guid CompanyId { get; set; }
    }
}
