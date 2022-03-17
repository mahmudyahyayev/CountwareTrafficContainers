using Mhd.Framework.Queue;
using System;

namespace CountwareTraffic.Services.Devices.Application
{
    public class MappedQueue
    {
        public string JsonData { get; set; }
        public Mhd.Framework.Queue.IQueueEvent Event { get; set; }
        public Type OriginalType { get; set; }
        public string ExchangeName { get; set; }
    }
}
