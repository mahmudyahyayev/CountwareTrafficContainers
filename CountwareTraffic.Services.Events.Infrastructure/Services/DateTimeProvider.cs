using CountwareTraffic.Services.Events.Application;
using System;

namespace CountwareTraffic.Services.Events
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
