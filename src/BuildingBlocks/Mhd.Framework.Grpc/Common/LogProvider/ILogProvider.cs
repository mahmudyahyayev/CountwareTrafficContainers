using Mhd.Framework.Ioc;
using System.Threading.Tasks;

namespace Mhd.Framework.Grpc.Common
{
    public interface ILogProvider : ISingletonDependency
    {
        public Task WriteLogAsync(WebServiceLog data);
    }
}
