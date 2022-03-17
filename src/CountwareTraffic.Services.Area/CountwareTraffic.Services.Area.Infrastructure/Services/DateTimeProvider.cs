using CountwareTraffic.Services.Areas.Application;
using System;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
