using System;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class OutboxMessageDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }
}
