using Convey.CQRS.Events;
using CountwareTraffic.Services.Areas.Core;
using Mhd.Framework.Queue;
using Mhd.Framework.QueueModel;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class SubAreaCreatedRejectedHandler : IEventHandler<SubAreaChangedRejected>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueService _queueService;
        public SubAreaCreatedRejectedHandler(IUnitOfWork unitOfWork, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _queueService = queueService;
        }

        public async Task HandleAsync(SubAreaChangedRejected command)
        {
            var subAreaRepository = _unitOfWork.GetRepository<ISubAreaRepository>();

            var subArea = await subAreaRepository.GetAsync(command.SubAreaId);

            subArea.WhenCreatedRejected();

            await _unitOfWork.CommitAsync();

            //SignalR ile frontendi besleme kismi
            _queueService.Publish(new SubAreaCreatedFailed
            {
                RecordId = Guid.NewGuid(),
                SubAreaId = command.SubAreaId,
                UserId = command.UserId,
                SubAreaStatus = SubAreaStatus.Rejected.Name,
                UserName = "Test" //todo
            });
        }
    }
}
