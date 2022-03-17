using System;

namespace Mhd.Framework.Api
{
    public class ServiceLog
    {
        public Guid Id { get; set; }
        public object MonitorLogId { get; set; }
        public dynamic Request { get; set; }
        public DateTime RequestTime { get; set; }
        public dynamic Response { get; set; }
        public DateTime ResponseTime { get; set; }
        public long? Duration { get; set; }
        public bool IsSuccess { get; set; }
    }
}