using Sensormatic.Tool.Ioc;

namespace Sensormatic.Tool.Cache
{
    public class RedisConfiguration : IConfigurationOptions
    {
        public string Connection { get; set; }
    }
}
