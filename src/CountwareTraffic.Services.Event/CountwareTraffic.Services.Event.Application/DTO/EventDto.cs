using System;

namespace CountwareTraffic.Services.Events.Application
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public Guid AuditCreateBy { get; set; }
    }
}
