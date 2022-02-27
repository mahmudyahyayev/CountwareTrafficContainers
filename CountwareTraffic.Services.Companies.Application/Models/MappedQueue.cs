using Sensormatic.Tool.Queue;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class MappedQueue
    {
        public string JsonData { get; set; }
        public IQueueEvent Event { get; set; }
        public Type OriginalType { get; set; }
        public string ExchangeName { get; set; }
    }
}
