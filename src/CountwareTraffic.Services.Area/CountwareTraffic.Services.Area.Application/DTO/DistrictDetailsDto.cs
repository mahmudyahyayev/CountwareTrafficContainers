using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public class DistrictDetailsDto : DistrictDto
    {
        public string Name { get; set; }
        public Guid CityId { get; set; }
    }
}
