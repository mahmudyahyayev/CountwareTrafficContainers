using Newtonsoft.Json;
using Mhd.Framework.Queue;
using System;
using System.Collections.Generic;

namespace Mhd.Framework.QueueModel
{
    public record AuditList<T> : MessageEnvelope, IQueueCommand
    {
        public AuditList()
        {
            Items = new List<T>();
            RecordId = Guid.NewGuid();
        }

        [JsonProperty("ITEMS")]
        public List<T> Items { get; set; }
        public Guid RecordId { get; init; }
    }
}
