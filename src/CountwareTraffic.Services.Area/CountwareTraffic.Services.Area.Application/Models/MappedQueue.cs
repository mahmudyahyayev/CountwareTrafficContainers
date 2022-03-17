using Mhd.Framework.Queue;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public class MappedQueue
    {
        public string JsonData { get; set; }
        public IQueueEvent Event { get; set; }
        public Type OriginalType { get; set; }
        public string ExchangeName { get; set; }
    }
}
