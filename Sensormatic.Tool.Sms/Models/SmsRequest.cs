using System.Collections.Generic;

namespace Sensormatic.Tool.Sms
{
    public class SmsRequest
    {
        public string Message { get; set; }
        public IList<string> PhoneNumbers { get; set; }
        public bool UseOTP { get; set; }
    }
}
