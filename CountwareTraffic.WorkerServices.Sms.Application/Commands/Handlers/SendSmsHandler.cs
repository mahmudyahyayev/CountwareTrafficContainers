using Convey.CQRS.Commands;
using CountwareTraffic.WorkerServices.Sms.Data;
using Sensormatic.Tool.Sms;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Sms.Application
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

                var jsonUserIds = Sensormatic.Tool.Common.TextJsonExtensions.Serialize(command.UserIds);
                var jsonPhoneNumbers = Sensormatic.Tool.Common.TextJsonExtensions.Serialize(command.PhoneNumbers);
                var jsonResult = Sensormatic.Tool.Common.TextJsonExtensions.Serialize(result);

                var smsLog = CountwareTraffic.WorkerServices.Sms.Data.SmsLog.Create(jsonUserIds, jsonPhoneNumbers, command.Message, jsonResult, command.IsOtp, SmsType.Default.Id);

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
