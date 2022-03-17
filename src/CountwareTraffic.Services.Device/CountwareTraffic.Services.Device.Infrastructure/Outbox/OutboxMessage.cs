using System;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class OutboxMessage
    {
        public Guid Id { get; init; }
        public Guid EventRecordId { get; set; }
        public DateTime OccurredOn { get; init; }
        public string Type { get; init; }
        public string Data { get; init; }
        public DateTime? ProcessedDate { get; set; }
        public string LastException { get; set; }
        public bool IsTryFromQueue { get; set; }
        private OutboxMessage() { }
        public OutboxMessage(DateTime occurredOn, string type, string data, Guid eventRecordId)
        {
            Id = Guid.NewGuid();
            OccurredOn = occurredOn;
            Type = type;
            Data = data;
            EventRecordId = eventRecordId;
            IsTryFromQueue = false;
        }
    }
}
