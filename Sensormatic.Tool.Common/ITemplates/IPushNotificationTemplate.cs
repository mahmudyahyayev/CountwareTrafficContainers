using System.Collections.Generic;

namespace Sensormatic.Tool.Common
{
    public interface IPushNotificationTemplate
    {
        public string TemplateName { get; }
        public List<string> PhoneNumbers { get; set; }
    }
}
