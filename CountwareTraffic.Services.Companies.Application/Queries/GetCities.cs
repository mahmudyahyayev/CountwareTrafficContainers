using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class GetCities : IQuery<PagingResult<CityDetailsDto>>
    {
        public Guid CountryId { get; set; }
        public PagingQuery PagingQuery { get; set; }
    }
}
