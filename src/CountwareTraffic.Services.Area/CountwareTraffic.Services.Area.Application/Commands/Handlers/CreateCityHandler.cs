using Convey.CQRS.Commands;
using CountwareTraffic.Services.Areas.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class CreateCityHandler : ICommandHandler<CreateCity>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(CreateCity command)
        {
            var cityRepository = _unitOfWork.GetRepository<ICityRepository>();

            if (!await _unitOfWork.GetRepository<ICountryRepository>().ExistsAsync(command.CountryId))
                throw new CountryNotFoundException(command.CountryId);

            if ((await cityRepository.ExistsAsync(command.Name)))
                throw new CityAlreadyExistsException(command.Name);

            var city = City.Create(command.Name, command.CountryId);

            await cityRepository.AddAsync(city);

            await _unitOfWork.CommitAsync();
        }
    }
}
