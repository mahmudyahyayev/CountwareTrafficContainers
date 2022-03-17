using System;
using System.Collections.Generic;

namespace Mhd.Framework.Common
{
    public class TestSmsTemplate : ISmsTemplate
    {
        public string TemplateName => SmsTemplates.CountwareTrafficTest;
        public List<string> PhoneNumbers { get; set; }
        public List<Guid> UserIds { get; set; }
        public string AnyData { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
