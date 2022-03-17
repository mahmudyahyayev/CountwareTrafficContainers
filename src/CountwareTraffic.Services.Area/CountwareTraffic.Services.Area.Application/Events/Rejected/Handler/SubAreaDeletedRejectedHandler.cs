using Convey.CQRS.Events;
using CountwareTraffic.Services.Areas.Core;
using Mhd.Framework.Queue;
using Mhd.Framework.QueueModel;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class SubAreaDeletedRejectedHandler : IEventHandler<SubAreaDeletedRejected>
    {
        private readonly IQueueService _queueService;
        private readonly IUnitOfWork _unitOfWork;
        public SubAreaDeletedRejectedHandler(IQueueService queueService, IUnitOfWork unitOfWork)
        {
            _queueService = queueService;
            _unitOfWork = unitOfWork;
        }


        public async  Task HandleAsync(SubAreaDeletedRejected command)
        {
            var subAreaRepository = _unitOfWork.GetRepository<ISubAreaRepository>();

            var subArea = await subAreaRepository.GetAsync(command.SubAreaId);

            subArea.WhenDeletedRejected();

            await _unitOfWork.CommitAsync();

            //SignalR ile frontendi besleme kismi
            _queueService.Publish(new SubAreaDeletedFailed
            {
                RecordId = Guid.NewGuid(),
                SubAreaId = command.SubAreaId,
                UserId = command.UserId,
                UserName = "Test" //todo
            });
        }
    }
}
