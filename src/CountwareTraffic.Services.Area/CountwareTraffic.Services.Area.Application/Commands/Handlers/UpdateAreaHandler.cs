using Convey.CQRS.Commands;
using CountwareTraffic.Services.Areas.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class UpdateAreaHandler : ICommandHandler<UpdateArea>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateAreaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(UpdateArea command)
        {
            var areaRepository = _unitOfWork.GetRepository<IAreaRepository>();

            var area = await areaRepository.GetAsync(command.AreaId);

            if (area is null)
                throw new AreaNotFoundException(command.AreaId);

            var address = AreaAddress.Create(command.Street, command.Latitude, command.Longitude);
            var contact = AreaContact.Create(command.GsmNumber, command.PhoneNumber, command.PhoneNumber2, command.EmailAddress, command.FaxNumber);

            area.CompleteChange(command.Name, command.Description, command.AreaTypeId, address, contact);

            await _unitOfWork.CommitAsync();
        }
    }
}
