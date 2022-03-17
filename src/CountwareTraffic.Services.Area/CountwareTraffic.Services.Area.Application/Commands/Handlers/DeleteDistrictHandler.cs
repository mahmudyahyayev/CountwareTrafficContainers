using Convey.CQRS.Commands;
using CountwareTraffic.Services.Areas.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class DeleteDistrictHandler : ICommandHandler<DeleteDistrict>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteDistrictHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeleteDistrict command)
        {
            var districtRepository = _unitOfWork.GetRepository<IDistrictRepository>();

            var district = await districtRepository.GetAsync(command.DistrictId);

            if (district is null) throw new DistrictNotFoundException(command.DistrictId);

            districtRepository.Remove(district);

            await _unitOfWork.CommitAsync();
        }
    }
}
