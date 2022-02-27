using System;
using System.Collections.Generic;

namespace Sensormatic.Tool.Common
{
    public class TestEmailTemplate : IEmailTemplate, IEmailReceiver
    {
        public string TemplateName => EmailTemplates.CountwareTrafficTest;

        public List<Guid> UserIds { get; set; }
        public List<string> To { get ; set ; }
        public List<string> Cc { get ; set ; }
        public List<string> Bc { get ; set ; }

        public string AnyData { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
