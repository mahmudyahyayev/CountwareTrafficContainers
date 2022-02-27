using Sensormatic.Tool.Ioc;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public interface IDateTimeProvider : ISingletonDependency
    {
        DateTime Now { get; }
    }
}
