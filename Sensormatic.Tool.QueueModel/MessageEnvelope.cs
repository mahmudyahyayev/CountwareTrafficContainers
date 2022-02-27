using System;

namespace Sensormatic.Tool.QueueModel
{
    public record MessageEnvelope
    {
        public Guid UserId { get; set; }
        public string CorrelationId { get; set; }
    }
}
