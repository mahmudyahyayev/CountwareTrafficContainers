using Mhd.Framework.Ioc;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Core
{
    public interface IUnitOfWork : IScopedDependency
    {
        T GetRepository<T>() where T : IRepository;
        int Commit();
        Task<int> CommitAsync();
    }
}
