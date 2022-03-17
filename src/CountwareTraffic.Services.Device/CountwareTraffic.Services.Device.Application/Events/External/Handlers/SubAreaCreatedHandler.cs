using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Core;
using Mhd.Framework.Queue;
using Mhd.Framework.QueueModel;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Application
{
    public class SubAreaCreatedHandler : IEventHandler<SubAreaCreated>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueService _queueService;

        public SubAreaCreatedHandler(IUnitOfWork unitOfWork, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _queueService = queueService;
        }

        public async Task HandleAsync(SubAreaCreated @event)
        {
            var subAreaRepository = _unitOfWork.GetRepository<ISubAreaRepository>();

            if (!await subAreaRepository.ExistsAsync(@event.SubAreaId))
            {
                try
                {
                    var subArea = SubArea.Create(@event.SubAreaId, @event.Name);

                    await subAreaRepository.AddAsync(subArea);

                    await _unitOfWork.CommitAsync();

                    _queueService.Publish(new SubAreaCreatedCompleted { SubAreaId = @event.SubAreaId, UserId = @event.UserId });
                }
                catch (Exception)
                {
                    _queueService.Publish(new SubAreaCreatedRejected { SubAreaId = @event.SubAreaId, UserId = @event.UserId });
                }
            }
        }
    }
}
