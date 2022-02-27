using Sensormatic.Tool.Ioc;

namespace CountwareTraffic.Services.Companies.Application
{
    public interface ICorrelationService : ITransientDependency
    {
        string CorrelationId { get; }
    }
}
