using System;
using Mhd.Framework.Ioc;

namespace CountwareTraffic.Services.Events.Application
{
    public interface IIdentityService : ITransientDependency
    {
         Guid UserId { get; }
    }
}
