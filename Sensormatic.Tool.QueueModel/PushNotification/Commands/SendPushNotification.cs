using Sensormatic.Tool.Queue;
using System;

namespace Sensormatic.Tool.QueueModel
{
    public record SendPushNotification : IQueueCommand
    {
        //suanda belli degil firebase insider falan ola bilir.
        public Guid RecordId { get ; init ; }
    }
}
