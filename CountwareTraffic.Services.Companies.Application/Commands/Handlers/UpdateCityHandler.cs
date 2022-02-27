using Convey.CQRS.Commands;
using CountwareTraffic.Services.Companies.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Application
{
    public class UpdateCityHandler : ICommandHandler<UpdateCity>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(UpdateCity command)
        {
            var cityRepository = _unitOfWork.GetRepository<ICityRepository>();

            var city = await cityRepository.GetAsync(command.CityId);

            if (city is null)
                throw new CityNotFoundException(command.CityId);

            city.CompleteChange(command.Name);

            await _unitOfWork.CommitAsync();
        }
    }
}
