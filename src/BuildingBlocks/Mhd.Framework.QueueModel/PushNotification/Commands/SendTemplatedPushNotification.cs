using Mhd.Framework.Queue;
using System;

namespace Mhd.Framework.QueueModel
{
    public record SendTemplatedPushNotification : IQueueCommand
    {
        //suanda belli degil firebase insider falan ola bilir.
        public Guid RecordId { get; init; }
    }
}
