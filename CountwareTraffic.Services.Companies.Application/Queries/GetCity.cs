using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class GetCity : IQuery<CityDetailsDto>
    {
        public Guid CityId { get; set; }
    }
}
