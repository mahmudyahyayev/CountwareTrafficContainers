using Convey.CQRS.Events;
using CountwareTraffic.Services.Areas.Core;
using Mhd.Framework.Queue;
using Mhd.Framework.QueueModel;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class SubAreaCreatedCompletedHandler : IEventHandler<SubAreaCreatedCompleted>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueService _queueService;
        public SubAreaCreatedCompletedHandler(IUnitOfWork unitOfWork, IQueueService queueService)
        {
            _unitOfWork = unitOfWork;
            _queueService = queueService;
        }

        public async Task HandleAsync(SubAreaCreatedCompleted command)
        {
            var subAreaRepository = _unitOfWork.GetRepository<ISubAreaRepository>();

            var subArea = await subAreaRepository.GetAsync(command.SubAreaId);

            subArea.WhenCreatedCompleted();

            await _unitOfWork.CommitAsync();

            //SignalR ile frontendi besleme kismi
            _queueService.Publish(new SubAreaCreatedSuccessfully
            {
                RecordId = Guid.NewGuid(),
                SubAreaId = command.SubAreaId,
                SubAreaStatus = SubAreaStatus.Completed.Name,
                UserId = command.UserId,
                UserName = "test"
            });
        }
    }    
}












//_queueService.Send(Queues.CountwareTrafficSendSms, new SendSms
//{
//    IsOtp = false,
//    Message = $"SubArea with id: {command.SubAreaId} has been created.",
//    PhoneNumbers = new List<string> { "5530878614" },
//    UserIds = new List<Guid> { command.UserId }
//});


//_queueService.Send(Queues.CountwareTrafficSendTemplatedSms, new SendTemplatedSms
//{
//    SmsTemplateAssemblyName = typeof(TestSmsTemplate).AssemblyQualifiedName,
//    SmsTemplate = new TestSmsTemplate
//    {
//        PhoneNumbers = new List<string> { "5530878614" },
//        UserIds = new List<Guid> { _identityService.UserId },
//        CreatedDate = DateTime.Now,
//        AnyData = "Templated sms denemesi"
//    }
//});
