using Mhd.Framework.Ioc;

namespace CountwareTraffic.Workers.Sms.Application
{
    public class SmsConfig : IConfigurationOptions
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OtpJobId { get; set; }
        public string JobId { get; set; }
    }
}
