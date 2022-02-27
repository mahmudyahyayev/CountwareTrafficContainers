using System;
using System.Collections.Generic;

namespace Sensormatic.Tool.Common
{
    public interface ISmsTemplate
    {
        public string TemplateName { get; }
        public List<string> PhoneNumbers { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
