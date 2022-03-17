using Convey.CQRS.Commands;
using CountwareTraffic.Workers.Email.Data;
using Mhd.Framework.Email;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Email.Application
{
    public class SendEmailHandler : ICommandHandler<SendEmail>
    {
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        public SendEmailHandler(IEmailService emaiService, IUnitOfWork unitOfWork)
        {
            _emailService = emaiService;
            _unitOfWork = unitOfWork;
        }
        public async Task HandleAsync(SendEmail command)
        {
            var result = await _emailService.SendAsync(new EmailRequest
            {
                Body = command.Body,
                Subject = command.Subject,
                To = command.To,
                IsHtml = command.IsHtml,
            });

            try
            {
                var emailLogRepository = _unitOfWork.GetRepository<IEmailLogRepository>();
                var jsonTo = Mhd.Framework.Common.TextJsonExtensions.Serialize(command.To);
                var jsonResult = Mhd.Framework.Common.TextJsonExtensions.Serialize(result);

                var emailLog = CountwareTraffic.Workers.Email.Data.EmailLog.Create(command.UserId.ToString(), command.Subject, command.Body, jsonTo, command.IsHtml, jsonResult, EmailType.Default.Id);

                await emailLogRepository.AddAsync(emailLog);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new DbLogException(ex.Message);
            }
        }
    }
}
