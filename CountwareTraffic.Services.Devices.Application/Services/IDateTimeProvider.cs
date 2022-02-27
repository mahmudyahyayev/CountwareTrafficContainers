using Sensormatic.Tool.Ioc;
using System;

namespace CountwareTraffic.Services.Devices.Application
{
    public interface IDateTimeProvider : ISingletonDependency
    {
        DateTime Now { get; }
    }
}
