using Convey.CQRS.Queries;

namespace CountwareTraffic.Services.Companies.Application
{
    public class GetCompanies : IQuery<PagingResult<CompanyDetailsDto>>
    {
        public PagingQuery PagingQuery { get; set; }
    }
}
