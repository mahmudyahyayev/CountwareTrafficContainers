using CountwareTraffic.Services.Companies.Application;
using System;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
