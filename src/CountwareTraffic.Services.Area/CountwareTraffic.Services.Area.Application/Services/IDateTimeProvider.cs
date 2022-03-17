using Mhd.Framework.Ioc;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public interface IDateTimeProvider : ISingletonDependency
    {
        DateTime Now { get; }
    }
}
