using Sensormatic.Tool.Ioc;

namespace CountwareTraffic.Services.Users.Application
{
    public class AuthenticationConfig : IConfigurationOptions
    {
        public string Authority { get; set; }
    }
}
