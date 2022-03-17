using Convey.CQRS.Commands;
using CountwareTraffic.Services.Areas.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class CreateDistrictHandler : ICommandHandler<CreateDistrict>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateDistrictHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(CreateDistrict command)
        {
            var districtRepository = _unitOfWork.GetRepository<IDistrictRepository>();

            if (!await _unitOfWork.GetRepository<ICityRepository>().ExistsAsync(command.CityId))
                throw new CityNotFoundException(command.CityId);

            if ((await districtRepository.ExistsAsync(command.Name)))
                throw new DistrictAlreadyExistsException(command.Name);

            var district = District.Create(command.Name, command.CityId);

            await districtRepository.AddAsync(district);

            await _unitOfWork.CommitAsync();
        }
    }
}
