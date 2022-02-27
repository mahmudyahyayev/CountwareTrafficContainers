using Sensormatic.Tool.Ioc;
using System;

namespace CountwareTraffic.Services.Events.Application
{
    public interface IDateTimeProvider : ISingletonDependency
    {
        DateTime Now { get; }
    }
}
