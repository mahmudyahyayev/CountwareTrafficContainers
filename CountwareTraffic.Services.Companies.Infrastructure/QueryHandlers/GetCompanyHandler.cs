using Convey.CQRS.Queries;
using CountwareTraffic.Services.Companies.Application;
using CountwareTraffic.Services.Companies.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class GetCustomerHandler : IQueryHandler<GetCompany, CompanyDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CompanyDetailsDto> HandleAsync(GetCompany query)
        {
            var company = await _unitOfWork.GetRepository<ICompanyRepository>()
                .GetAsync(query.CompanyId);

            if (company == null)
                throw new CompanyNotFoundException(query.CompanyId);

            return new CompanyDetailsDto
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                AuditCreateBy = company.AuditCreateBy,
                AuditCreateDate = company.AuditCreateDate,
                AuditModifiedBy = company.AuditModifiedBy,
                AuditModifiedDate = company.AuditModifiedDate,
                EmailAddress = company.Contact.EmailAddress,
                FaxNumber = company.Contact.FaxNumber,
                GsmNumber = company.Contact.GsmNumber,
                PhoneNumber = company.Contact.PhoneNumber,
                City = company.Address.City,
                Latitude = company.Address.Location?.Y,
                Longitude = company.Address.Location?.X,
                Country = company.Address.Country,
                State = company.Address.State,
                Street = company.Address.Street,
                ZipCode = company.Address.ZipCode
            };
        }
    }
}
