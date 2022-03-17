using Mhd.Framework.Ioc;

namespace CountwareTraffic.Services.Areas.Application
{
    public interface ICorrelationService : ITransientDependency
    {
        string CorrelationId { get; }
    }
}
