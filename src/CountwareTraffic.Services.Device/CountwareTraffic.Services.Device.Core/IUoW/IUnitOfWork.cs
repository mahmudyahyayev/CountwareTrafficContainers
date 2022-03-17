using System.Threading.Tasks;
using Mhd.Framework.Ioc;

namespace CountwareTraffic.Services.Devices.Core
{
    public interface IUnitOfWork :IScopedDependency
    {
        T GetRepository<T>() where T : IRepository;
        int Commit();
        Task<int> CommitAsync();
    }
}
