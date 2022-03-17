using Convey.CQRS.Commands;
using CountwareTraffic.Services.Areas.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class UpdateDistrictHandler : ICommandHandler<UpdateDistrict>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateDistrictHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(UpdateDistrict command)
        {
            var districtRepository = _unitOfWork.GetRepository<IDistrictRepository>();

            var district = await districtRepository.GetAsync(command.DistrictId);

            if (district is null)
                throw new DistrictNotFoundException(command.DistrictId);

            district.CompleteChange(command.Name);

            await _unitOfWork.CommitAsync();
        }
    }
}
