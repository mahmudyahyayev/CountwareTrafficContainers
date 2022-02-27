using System;
using Sensormatic.Tool.Ioc;

namespace CountwareTraffic.Services.Events.Application
{
    public interface IIdentityService : ITransientDependency
    {
         Guid UserId { get; }
    }
}
