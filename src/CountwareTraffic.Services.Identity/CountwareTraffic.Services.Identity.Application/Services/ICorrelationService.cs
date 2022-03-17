using Mhd.Framework.Ioc;

namespace CountwareTraffic.Services.Identity.Application
{
    public interface ICorrelationService : ITransientDependency
    {
        string CorrelationId { get; }
    }
}
