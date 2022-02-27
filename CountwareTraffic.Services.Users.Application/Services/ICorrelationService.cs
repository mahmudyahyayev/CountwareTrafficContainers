using Sensormatic.Tool.Ioc;

namespace CountwareTraffic.Services.Users.Application
{
    public interface ICorrelationService : ITransientDependency
    {
        string CorrelationId { get; }
    }
}
