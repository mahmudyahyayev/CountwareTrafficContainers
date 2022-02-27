using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class GetCountry : IQuery<CountryDetailsDto>
    {
        public Guid CountryId { get; set; }
    }
}
