using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public class CityDetailsDto : CityDto
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
