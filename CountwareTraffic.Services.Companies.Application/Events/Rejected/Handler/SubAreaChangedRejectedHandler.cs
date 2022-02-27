using Convey.CQRS.Events;
using CountwareTraffic.Services.Companies.Core;
using System.Threading.Tasks;
using Sensormatic.Tool.Queue;
using Sensormatic.Tool.QueueModel;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class SubAreaChangedRejectedHandler : IEventHandler<SubAreaChangedRejected>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueService _queueService;
        public SubAreaChangedRejectedHandler(IUnitOfWork unitOfWork, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _queueService = queueService;
        }

        public async Task HandleAsync(SubAreaChangedRejected command)
        {
            var subAreaRepository = _unitOfWork.GetRepository<ISubAreaRepository>();

            var subArea = await subAreaRepository.GetAsync(command.SubAreaId);

            subArea.WhenChangedRejected(command.OldName);

            await _unitOfWork.CommitAsync();

            //SignalR ile frontendi besleme kismi
            _queueService.Publish(new SubAreaChangedFailed
            {
                RecordId = Guid.NewGuid(),
                SubAreaId = command.SubAreaId,
                OldName = command.OldName,
                UserId = command.UserId,
                NewName = command.Name,
                 UserName = "Test" //todo
            });
        }
    }
}
