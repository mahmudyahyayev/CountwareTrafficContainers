using System;
using Mhd.Framework.Ioc;

namespace CountwareTraffic.Services.Devices.Application
{
    public interface IIdentityService :ITransientDependency
    {
         Guid UserId { get; }
    }
}
