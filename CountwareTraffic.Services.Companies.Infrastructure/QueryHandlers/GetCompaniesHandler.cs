using Convey.CQRS.Queries;
using CountwareTraffic.Services.Companies.Application;
using CountwareTraffic.Services.Companies.Core;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class GetCompaniesHandler : IQueryHandler<GetCompanies, PagingResult<CompanyDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompaniesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagingResult<CompanyDetailsDto>> HandleAsync(GetCompanies query)
        {
            int page = query.PagingQuery.Page;
            int limit = query.PagingQuery.Limit;

            var qres = await _unitOfWork
                                        .GetRepository<ICompanyRepository>()
                                        .GetAllAsync(page, limit);

            if (qres == null)
                return PagingResult<CompanyDetailsDto>.Empty;

            bool hasNextPage = qres.Total > (limit * (page - 1)) + qres.Entities.Count;

            return new PagingResult<CompanyDetailsDto>(qres.Entities.Select(x => new CompanyDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                AuditCreateBy = x.AuditCreateBy,
                AuditCreateDate = x.AuditCreateDate,
                AuditModifiedBy = x.AuditModifiedBy,
                AuditModifiedDate = x.AuditModifiedDate,
                City = x.Address.City,
                Country = x.Address.Country,
                 Latitude = x.Address.Location?.Y,
                Longitude = x.Address.Location?.X,
                State = x.Address.State,
                Street = x.Address.Street,
                ZipCode = x.Address.ZipCode,
                EmailAddress = x.Contact.EmailAddress,
                FaxNumber = x.Contact.FaxNumber,
                GsmNumber = x.Contact.GsmNumber,
                PhoneNumber = x.Contact.PhoneNumber,
            }), qres.Total, page, limit, hasNextPage);
        }
    }
}