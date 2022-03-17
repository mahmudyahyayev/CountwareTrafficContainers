using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public class GetDistricts : IQuery<PagingResult<DistrictDetailsDto>>
    {
        public Guid CityId { get; set; }
        public PagingQuery PagingQuery { get; set; }
    }
}
