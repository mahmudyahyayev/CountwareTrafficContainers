using Mhd.Framework.Ioc;

namespace CountwareTraffic.Services.Events.Application
{
    public interface ICorrelationService : ITransientDependency
    {
        string CorrelationId { get; }
    }
}
