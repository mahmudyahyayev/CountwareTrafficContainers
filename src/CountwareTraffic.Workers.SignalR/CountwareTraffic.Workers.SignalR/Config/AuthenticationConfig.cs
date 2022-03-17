using Mhd.Framework.Ioc;

namespace CountwareTraffic.Workers.SignalR
{
    public class AuthenticationConfig : IConfigurationOptions
    {
        public string Audience { get; set; }
        public string SignKey { get; set; }
    }
}
