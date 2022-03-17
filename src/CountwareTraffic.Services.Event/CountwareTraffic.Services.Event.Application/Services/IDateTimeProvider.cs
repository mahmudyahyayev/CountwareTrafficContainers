using Mhd.Framework.Ioc;
using System;

namespace CountwareTraffic.Services.Events.Application
{
    public interface IDateTimeProvider : ISingletonDependency
    {
        DateTime Now { get; }
    }
}
