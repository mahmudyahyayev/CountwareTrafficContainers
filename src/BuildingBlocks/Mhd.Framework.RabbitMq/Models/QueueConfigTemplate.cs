using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Mhd.Framework.Queue
{
    public class QueueConfigTemplate
    {
        public ushort PrefetchCount { get; set; }
        public ushort RetryCount { get; set; }
        public int RetryIntervalSeconds { get; set; }
        public List<Type> ExcludeExceptions { get; set; }
        public bool AutoScale { get; set; }
        public ushort ScaleUpTo { get; set; }

        public static QueueConfigTemplate Default()
        {
            return new QueueConfigTemplate
            {
                PrefetchCount = 1,
                RetryCount = 2, 
                RetryIntervalSeconds = 120,
                ExcludeExceptions = new List<Type> { typeof(Exception), typeof(HttpRequestException), typeof(ArgumentNullException) },
                AutoScale = true,
                ScaleUpTo = 10
            };
        }
    }
}
