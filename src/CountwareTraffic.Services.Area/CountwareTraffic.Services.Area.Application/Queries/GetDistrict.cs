using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public class GetDistrict : IQuery<DistrictDetailsDto>
    {
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
    }
}
