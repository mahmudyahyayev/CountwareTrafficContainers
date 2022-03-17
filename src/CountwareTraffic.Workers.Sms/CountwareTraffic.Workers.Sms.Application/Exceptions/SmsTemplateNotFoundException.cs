using Mhd.Framework.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Workers.Sms.Application
{
    public class SmsTemplateNotFoundException : AppException
    {
        public string TemplateName { get; set; }
        public SmsTemplateNotFoundException(string templateName)
            : base(new List<ErrorResult>() { new ErrorResult($"SmsTemplate with name: {templateName} was not found") }, 404, ResponseMessageType.Error)
        {
            TemplateName = templateName;
        }
    }
}
