using Newtonsoft.Json;
using Sensormatic.Tool.Queue;
using System;
using System.Collections.Generic;

namespace Sensormatic.Tool.QueueModel
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
