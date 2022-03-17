using Mhd.Framework.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Workers.Email.Application
{
    public class EmailTemplateNotFoundException : AppException
    {
        public string TemplateName { get; set; }
        public EmailTemplateNotFoundException(string templateName)
            : base(new List<ErrorResult>() { new ErrorResult($"EmailTemplate with name: {templateName} was not found") }, 404, ResponseMessageType.Error)
        {
            TemplateName = templateName;
        }
    }
}
