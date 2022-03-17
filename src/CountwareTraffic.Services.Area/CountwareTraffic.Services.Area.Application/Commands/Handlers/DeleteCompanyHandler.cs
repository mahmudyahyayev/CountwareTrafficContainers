using Convey.CQRS.Commands;
using CountwareTraffic.Services.Areas.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class DeleteCompanyHandler : ICommandHandler<DeleteCompany>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeleteCompany command)
        {
            var companyRepository = _unitOfWork.GetRepository<ICompanyRepository>();

            var company = await companyRepository.GetAsync(command.CompanyId);

            if (company is null) throw new CompanyNotFoundException(command.CompanyId);

            companyRepository.Remove(company);

            await _unitOfWork.CommitAsync();
        }
    }
}
