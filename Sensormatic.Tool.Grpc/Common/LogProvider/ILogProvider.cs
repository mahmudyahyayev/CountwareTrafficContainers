using Sensormatic.Tool.Ioc;
using System.Threading.Tasks;

namespace Sensormatic.Tool.Grpc.Common
{
    public interface ILogProvider : ISingletonDependency
    {
        public Task WriteLogAsync(WebServiceLog data);
    }
}
