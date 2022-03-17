using System;

namespace Mhd.Framework.Api
{
    public class MonitorLog
    {
        public Guid Id { get; set; }
        public string ActionName { get; set; }
        public DateTime ActiveDate { get; set; }
    }
}