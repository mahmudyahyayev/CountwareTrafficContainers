using Mhd.Framework.Ioc;

namespace CountwareTraffic.Services.Identity.Application
{
    public class AuthenticationConfig : IConfigurationOptions
    {
        public string Authority { get; set; }
    }
}
