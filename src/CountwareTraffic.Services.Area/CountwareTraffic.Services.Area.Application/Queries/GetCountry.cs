using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public class GetCountry : IQuery<CountryDetailsDto>
    {
        public Guid CountryId { get; set; }
    }
}
