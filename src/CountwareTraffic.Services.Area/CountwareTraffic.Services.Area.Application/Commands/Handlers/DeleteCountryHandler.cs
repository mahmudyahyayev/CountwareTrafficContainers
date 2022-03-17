using Convey.CQRS.Commands;
using CountwareTraffic.Services.Areas.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class DeleteCountryHandler : ICommandHandler<DeleteCountry>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCountryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeleteCountry command)
        {
            var countryRepository = _unitOfWork.GetRepository<ICountryRepository>();

            var country = await countryRepository.GetAsync(command.CountryId);

            if (country is null) throw new CountryNotFoundException(command.CountryId);

            countryRepository.Remove(country);

            await _unitOfWork.CommitAsync();
        }
    }
}
