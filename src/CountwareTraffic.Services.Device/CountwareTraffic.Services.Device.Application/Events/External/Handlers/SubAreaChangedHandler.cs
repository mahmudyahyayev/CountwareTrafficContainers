using Convey.CQRS.Events;
using CountwareTraffic.Services.Devices.Core;
using Mhd.Framework.Queue;
using System;
using System.Threading.Tasks;
using Mhd.Framework.QueueModel;

namespace CountwareTraffic.Services.Devices.Application
{
    public class SubAreaChangedHandler : IEventHandler<SubAreaChanged>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueService _queueService;
        public SubAreaChangedHandler(IUnitOfWork unitOfWork, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _queueService = queueService;
        }

        public async Task HandleAsync(SubAreaChanged @event)
        {
            var subArea = await _unitOfWork.GetRepository<ISubAreaRepository>()
                 .GetAsync(@event.SubAreaId);

            if (subArea != null)
            {
                try
                {
                    subArea.Change(@event.Name);
                    await _unitOfWork.CommitAsync();

                    //SignalR ile frontendi besleme kismi
                    _queueService.Publish(new SubAreaChangedSuccessfully
                    {
                        RecordId = Guid.NewGuid(),
                        NewName = @event.Name,
                        OldName = @event.OldName,
                        SubAreaId = @event.SubAreaId,
                        UserId = @event.UserId,
                        UserName = "Test" //todo
                    });
                }
                catch (Exception ex)
                {
                    _queueService.Publish(new SubAreaChangedRejected { SubAreaId = @event.SubAreaId, Name = @event.Name, OldName = @event.OldName, UserId = @event.UserId });
                }
            }
        }
    }
}
