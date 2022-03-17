using CountwareTraffic.Services.Devices.Application;
using System;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
