using Sensormatic.Tool.Ioc;

namespace CountwareTraffic.WorkerServices.SignalrHub
{
    public class AuthenticationConfig : IConfigurationOptions
    {
        public string Audience { get; set; }
        public string SignKey { get; set; }
    }
}
