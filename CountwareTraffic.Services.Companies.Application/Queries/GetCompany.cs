using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class GetCompany : IQuery<CompanyDetailsDto>
    {
        public Guid CompanyId { get; set; }
    }
}
