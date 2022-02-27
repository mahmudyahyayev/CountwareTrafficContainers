using Sensormatic.Tool.Ioc;

namespace CountwareTraffic.WorkerServices.Email.Application
{
    public class EmailConfig : IConfigurationOptions
    {
        public string SMTPServer { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPMailFrom { get; set; }
    }
}
