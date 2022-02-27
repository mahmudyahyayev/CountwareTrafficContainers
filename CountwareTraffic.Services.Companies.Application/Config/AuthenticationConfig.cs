using Sensormatic.Tool.Ioc;

namespace CountwareTraffic.Services.Companies.Application
{
    public class AuthenticationConfig : IConfigurationOptions
    {
        public string Audience { get; set; }
        public string SignKey { get; set; }
    }
}
