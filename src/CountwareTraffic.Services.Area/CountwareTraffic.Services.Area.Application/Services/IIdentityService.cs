using Mhd.Framework.Ioc;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public interface IIdentityService : ITransientDependency
    {
         Guid UserId { get; }
    }
}
