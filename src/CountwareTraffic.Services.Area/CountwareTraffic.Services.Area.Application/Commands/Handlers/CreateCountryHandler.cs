using Convey.CQRS.Commands;
using CountwareTraffic.Services.Areas.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class CreateCountryHandler : ICommandHandler<CreateCountry>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCountryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(CreateCountry command)
        {
            var countryRepository = _unitOfWork.GetRepository<ICountryRepository>();

            if (!await _unitOfWork.GetRepository<ICompanyRepository>().ExistsAsync(command.CompanyId))
                throw new CompanyNotFoundException(command.CompanyId);

            if ((await countryRepository.ExistsAsync(command.Name)))
                throw new CountryAlreadyExistsException(command.Name);

            var country = Country.Create(command.Iso, command.Iso3, command.IsoNumeric, command.Name, command.Capital, command.ContinentCode, command.CurrencyCode, command.CompanyId);

            await countryRepository.AddAsync(country);

            await _unitOfWork.CommitAsync();
        }
    }
}
