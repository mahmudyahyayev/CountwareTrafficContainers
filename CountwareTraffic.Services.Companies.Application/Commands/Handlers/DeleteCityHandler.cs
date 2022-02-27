using Convey.CQRS.Commands;
using CountwareTraffic.Services.Companies.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Application
{
    public class DeleteCityHandler : ICommandHandler<DeleteCity>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeleteCity command)
        {
            var cityRepository = _unitOfWork.GetRepository<ICityRepository>();

            var city = await cityRepository.GetAsync(command.CityId);

            if (city is null) throw new CityNotFoundException(command.CityId);

            cityRepository.Remove(city);

            await _unitOfWork.CommitAsync();
        }
    }
}
