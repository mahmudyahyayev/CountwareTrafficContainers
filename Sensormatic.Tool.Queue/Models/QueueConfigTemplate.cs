using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Sensormatic.Tool.Queue
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
                RetryCount = 2, //Bir kere kendi calisma eger asagidaki hatalardan her hangi firlatilmasza farkli hata firlatilirsa 2 kere daha tekrarlar hala hatalar cozulmemisse fault kuyruguna duser. 
                RetryIntervalSeconds = 120,
                ExcludeExceptions = new List<Type> { typeof(Exception), typeof(HttpRequestException), typeof(ArgumentNullException) },
                //Hata olarak json formatinin dogrulugu da kontrol edile bilir eger json formatda sorun varsa direk retry calismasin.
                //Yukaridaki hatalardan her hangi biri ciksa zaten retry calismammasi lazim.
                AutoScale = true,
                ScaleUpTo = 10
            };
        }
    }
}
