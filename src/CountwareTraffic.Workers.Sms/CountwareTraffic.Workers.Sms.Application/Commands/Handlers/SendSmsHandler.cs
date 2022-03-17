using Convey.CQRS.Commands;
using CountwareTraffic.Workers.Sms.Data;
using Mhd.Framework.Sms;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Sms.Application
{
    public class SendSmsHandler : ICommandHandler<SendSms>
    {
        private readonly ISmsService _smsService;
        private readonly IUnitOfWork _unitOfWork;
        public SendSmsHandler(ISmsService smsService, IUnitOfWork unitOfWork)
        {
            _smsService = smsService;
            _unitOfWork = unitOfWork;
        }
        public async Task HandleAsync(SendSms command)
        {
            var result = await _smsService.SendAsync(new SmsRequest
            {
                Message = command.Message,
                PhoneNumbers = command.PhoneNumbers,
                UseOTP = command.IsOtp
            });

            try
            {
                var smsLogRepository = _unitOfWork.GetRepository<ISmsLogRepository>();

                var jsonUserIds = Mhd.Framework.Common.TextJsonExtensions.Serialize(command.UserIds);
                var jsonPhoneNumbers = Mhd.Framework.Common.TextJsonExtensions.Serialize(command.PhoneNumbers);
                var jsonResult = Mhd.Framework.Common.TextJsonExtensions.Serialize(result);

                var smsLog = CountwareTraffic.Workers.Sms.Data.SmsLog.Create(jsonUserIds, jsonPhoneNumbers, command.Message, jsonResult, command.IsOtp, SmsType.Default.Id);

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
