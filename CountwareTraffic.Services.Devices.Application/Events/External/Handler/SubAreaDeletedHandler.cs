using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Core;
using Sensormatic.Tool.Queue;
using System;
using Sensormatic.Tool.QueueModel;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Application
{
    public class SubAreaDeletedHandler : IEventHandler<SubAreaDeleted>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueService _queueService;
        public SubAreaDeletedHandler(IUnitOfWork unitOfWork, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _queueService = queueService;
        }

        public async Task HandleAsync(SubAreaDeleted @event)
        {
            var subAreaRepository = _unitOfWork.GetRepository<ISubAreaRepository>();

            var subArea = await subAreaRepository.GetAsync(@event.SubAreaId);

            if (subArea != null)
            {
                try
                {
                    subAreaRepository.Remove(subArea);
                    await _unitOfWork.CommitAsync();

                    //SignalR ile frontendi besleme kismi
                    _queueService.Publish(new SubAreaDeletedSuccessfully
                    {
                        RecordId = Guid.NewGuid(),
                        SubAreaId = @event.SubAreaId,
                        UserId = @event.UserId,
                        UserName = "Test" //todo
                    });
                }
                catch (Exception ex)
                {
                    _queueService.Publish(new SubAreaDeletedRejected { SubAreaId = @event.SubAreaId , UserId = @event.UserId });
                }
            }
        }
    }
}