using Sensormatic.Tool.Ioc;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public interface IIdentityService : ITransientDependency
    {
         Guid UserId { get; }
    }
}
