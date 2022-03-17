using Mhd.Framework.Ioc;

namespace Mhd.Framework.Cache
{
    public class RedisConfiguration : IConfigurationOptions
    {
        public string Connection { get; set; }
    }
}
