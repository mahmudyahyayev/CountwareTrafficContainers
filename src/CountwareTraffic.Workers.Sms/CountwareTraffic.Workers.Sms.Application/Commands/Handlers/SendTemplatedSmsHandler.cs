using Convey.CQRS.Commands;
using CountwareTraffic.Workers.Sms.Data;
using Mhd.Framework.Sms;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Sms.Application
{
    public class SendTemplatedSmsHandler : ICommandHandler<SendTemplatedSms>
    {
        private readonly ISmsService _smsService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISmsTemplateMongoDbRepository _smsTemplateMongoDbRepository;
        private readonly IRendererService _rendererService;
        public SendTemplatedSmsHandler(ISmsService smsService, IUnitOfWork unitOfWork, ISmsTemplateMongoDbRepository smsTemplateMongoDbRepository, IRendererService rendererService)
        {
            _smsService = smsService;
            _unitOfWork = unitOfWork;
            _smsTemplateMongoDbRepository = smsTemplateMongoDbRepository;
            _rendererService = rendererService;
        }

        public async Task HandleAsync(SendTemplatedSms command)
        {
            var templatedSms =  await _smsTemplateMongoDbRepository.GetAsync(command.Template.TemplateName);

            if (templatedSms == null)
                throw new SmsTemplateNotFoundException(command.Template.TemplateName);

            var message = await _rendererService.RenderAsync(templatedSms.Template, command.Template);

            var result = await _smsService.SendAsync(new SmsRequest { Message = message, PhoneNumbers = command.Template.PhoneNumbers, UseOTP = templatedSms.IsOtp });

            try
            {
                var smsLogRepository = _unitOfWork.GetRepository<ISmsLogRepository>();

                var jsonUserIds = Mhd.Framework.Common.TextJsonExtensions.Serialize(command.Template.UserIds);
                var jsonPhoneNumbers = Mhd.Framework.Common.TextJsonExtensions.Serialize(command.Template.PhoneNumbers);
                var jsonResult = Mhd.Framework.Common.TextJsonExtensions.Serialize(result);

                var smsLog = CountwareTraffic.Workers.Sms.Data.SmsLog.Create(jsonUserIds, jsonPhoneNumbers, message, jsonResult, templatedSms.IsOtp, SmsType.Templated.Id);

                await smsLogRepository.AddAsync(smsLog);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new DbLogException(ex.Message);
            }
        }
    }
}




// await _smsTemplateMongoDbRepository.AddAsync( new SmsTemplate { Description = "Description", Type = command.Template.TemplateName, IsOtp = false, Template =  "Bu bir test sms template denemesidir"});