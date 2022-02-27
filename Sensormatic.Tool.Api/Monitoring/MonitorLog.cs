using System;

namespace Sensormatic.Tool.Api
{
    public class MonitorLog
    {
        public Guid Id { get; set; }
        public string ActionName { get; set; }
        public DateTime ActiveDate { get; set; }
    }
}