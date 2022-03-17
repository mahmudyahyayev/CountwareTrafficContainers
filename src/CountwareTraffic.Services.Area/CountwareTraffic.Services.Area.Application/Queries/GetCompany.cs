using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public class GetCompany : IQuery<CompanyDetailsDto>
    {
        public Guid CompanyId { get; set; }
    }
}
