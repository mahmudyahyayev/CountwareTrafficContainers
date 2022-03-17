using System.Collections.Generic;

namespace Mhd.Framework.Common
{
    public interface IPushNotificationTemplate
    {
        public string TemplateName { get; }
        public List<string> PhoneNumbers { get; set; }
    }
}
