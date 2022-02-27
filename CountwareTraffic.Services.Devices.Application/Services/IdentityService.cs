using System;
using Sensormatic.Tool.Ioc;

namespace CountwareTraffic.Services.Devices.Application
{
    public interface IIdentityService :ITransientDependency
    {
         Guid UserId { get; }
    }
}
