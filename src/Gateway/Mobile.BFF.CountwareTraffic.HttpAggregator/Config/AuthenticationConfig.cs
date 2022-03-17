using Mhd.Framework.Ioc;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class AuthenticationConfig : IConfigurationOptions
    {
        public string Audience { get; set; }
        public string SignKey { get; set; }
    }
}
