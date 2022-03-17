using System;

namespace Mhd.Framework.QueueModel
{
    public record MessageEnvelope
    {
        public Guid UserId { get; set; }
        public string CorrelationId { get; set; }
    }
}
