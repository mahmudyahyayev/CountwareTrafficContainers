using Mhd.Framework.Ioc;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class UrlsConfig :  IConfigurationOptions
    {
        public string GrpcArea { get; set; }
        public string GrpcDevice { get; set; }
        public string GrpcUser { get; set; }
    }
}
