using Convey.CQRS.Commands;
using CountwareTraffic.Services.Companies.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Application
{
    public class CreateCompanyHandler : ICommandHandler<CreateCompany>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(CreateCompany command)
        {
            var companyRepository = _unitOfWork.GetRepository<ICompanyRepository>();

            if (await companyRepository.ExistsAsync(command.Name))
                throw new CompanyAlreadyExistsException(command.Name);

            var address = CompanyAddress.Create(command.Street, command.City, command.State, command.Country, command.ZipCode, command.Latitude, command.Longitude);
            var contract = CompanyContact.Create(command.GsmNumber, command.PhoneNumber, command.EmailAddress, command.FaxNumber);

            var company = Company.Create(command.Name, command.Description, address, contract);

            await companyRepository.AddAsync(company);

            await _unitOfWork.CommitAsync();
        }
    }
}
