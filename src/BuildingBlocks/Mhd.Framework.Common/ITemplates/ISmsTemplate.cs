using System;
using System.Collections.Generic;

namespace Mhd.Framework.Common
{
    public interface ISmsTemplate
    {
        public string TemplateName { get; }
        public List<string> PhoneNumbers { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
