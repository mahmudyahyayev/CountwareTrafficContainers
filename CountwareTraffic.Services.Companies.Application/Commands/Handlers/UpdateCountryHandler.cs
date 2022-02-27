using Convey.CQRS.Commands;
using CountwareTraffic.Services.Companies.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Application
{
    public class UpdateCountryHandler : ICommandHandler<UpdateCountry>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCountryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(UpdateCountry command)
        {
            var countryRepository = _unitOfWork.GetRepository<ICountryRepository>();

            var country = await countryRepository.GetAsync(command.CountryId);

            if (country is null)
                throw new CountryNotFoundException(command.CountryId);

            country.CompleteChange(command.Iso, command.Iso3, command.IsoNumeric, command.Name, command.Capital, command.ContinentCode, command.CurrencyCode);

            await _unitOfWork.CommitAsync();
        }
    }
}
