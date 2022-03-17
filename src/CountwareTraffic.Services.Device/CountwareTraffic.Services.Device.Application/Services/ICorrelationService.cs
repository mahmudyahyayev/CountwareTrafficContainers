using Mhd.Framework.Ioc;

namespace CountwareTraffic.Services.Devices.Application
{
    public interface ICorrelationService : ITransientDependency
    {
        string CorrelationId { get; }
    }
}
