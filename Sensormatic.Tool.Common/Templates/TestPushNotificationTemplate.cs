using System.Collections.Generic;

namespace Sensormatic.Tool.Common
{
    public class TestPushNotificationTemplate : IPushNotificationTemplate
    {
        public string TemplateName => PushNotificationTemplates.CountwareTrafficTest;

        public List<string> PhoneNumbers { get; set; }

        public int AnyData { get; set; }
    }
}
