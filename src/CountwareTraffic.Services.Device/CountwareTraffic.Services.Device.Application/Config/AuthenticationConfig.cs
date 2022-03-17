using Mhd.Framework.Ioc;

namespace CountwareTraffic.Services.Devices.Application
{
    public class AuthenticationConfig : IConfigurationOptions
    {
        public string Audience { get; set; }
        public string SignKey { get; set; }
    }
}
