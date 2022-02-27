using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class CityDetailsDto : CityDto
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
