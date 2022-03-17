using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public class GetCity : IQuery<CityDetailsDto>
    {
        public Guid CityId { get; set; }
    }
}
