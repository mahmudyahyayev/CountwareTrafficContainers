using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class GetCountries : IQuery<PagingResult<CountryDetailsDto>>
    {
        public Guid CompanyId { get; set; }
        public PagingQuery PagingQuery { get; set; }
    }
}
