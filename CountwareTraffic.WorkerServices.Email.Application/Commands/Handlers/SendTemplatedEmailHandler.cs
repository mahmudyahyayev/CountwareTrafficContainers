﻿using Convey.CQRS.Commands;
using CountwareTraffic.WorkerServices.Email.Data;
using Sensormatic.Tool.Common;
using Sensormatic.Tool.Email;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Email.Application
{
    public class SendTemplatedEmailHandler : ICommandHandler<SendTemplatedEmail>
    {
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailTemplateMongoDbRepository _emailTemplateMongoDbRepository;
        private readonly IRendererService _rendererService;
        public SendTemplatedEmailHandler(IEmailService emailService, IUnitOfWork unitOfWork, IEmailTemplateMongoDbRepository emailTemplateMongoDbRepository, IRendererService rendererService)
        {
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _emailTemplateMongoDbRepository = emailTemplateMongoDbRepository;
            _rendererService = rendererService;
        }

        public async Task HandleAsync(SendTemplatedEmail command)
        {
            var hasReceiverInfo = command.Template.GetType().GetInterfaces()
               .Where(x => x == typeof(IEmailReceiver))
               .Any();

            var templatedEmail = await _emailTemplateMongoDbRepository.GetAsync(command.Template.TemplateName);

            if (templatedEmail == null)
                throw new EmailTemplateNotFoundException(command.Template.TemplateName);

            var subject = await _rendererService.RenderAsync(templatedEmail.SubjectTemplate, command.Template);

            var body = await _rendererService.RenderAsync(templatedEmail.BodyTemplate, command.Template);

            var to = hasReceiverInfo ? ((IEmailReceiver)command.Template).To : templatedEmail.To;


            var result = await _emailService.SendAsync(new EmailRequest
            {
                Body = body,
                Subject = subject,
                To = to,
                IsHtml = templatedEmail.IsHtml
            });

            try
            {
                var emailLogRepository = _unitOfWork.GetRepository<IEmailLogRepository>();
                var jsonUserIds = Sensormatic.Tool.Common.TextJsonExtensions.Serialize(command.Template.UserIds);
                var jsonTo = Sensormatic.Tool.Common.TextJsonExtensions.Serialize(to);
                var jsonResult = Sensormatic.Tool.Common.TextJsonExtensions.Serialize(result);

                var emailLog = CountwareTraffic.WorkerServices.Email.Data.EmailLog.Create(jsonUserIds, subject, body, jsonTo, templatedEmail.IsHtml, jsonResult, EmailType.Default.Id);

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