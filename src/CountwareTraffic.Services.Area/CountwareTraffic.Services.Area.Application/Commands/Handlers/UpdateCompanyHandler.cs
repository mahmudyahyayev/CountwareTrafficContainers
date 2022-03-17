using Convey.CQRS.Commands;
using CountwareTraffic.Services.Areas.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class UpdateCompanyHandler : ICommandHandler<UpdateCompany>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(UpdateCompany command)
        {
            var companyRepository = _unitOfWork.GetRepository<ICompanyRepository>();

            var company = await companyRepository.GetAsync(command.ComapnyId);

            if (company is null)
                throw new CompanyNotFoundException(command.ComapnyId);

            var address = CompanyAddress.Create(command.Street, command.City, command.State, command.Country, command.ZipCode, command.Latitude, command.Longitude);
            var contact = CompanyContact.Create(command.GsmNumber, command.PhoneNumber, command.EmailAddress, command.FaxNumber);

            company.CompleteChange(command.Name, command.Description, address, contact);

            await _unitOfWork.CommitAsync();
        }
    }
}
