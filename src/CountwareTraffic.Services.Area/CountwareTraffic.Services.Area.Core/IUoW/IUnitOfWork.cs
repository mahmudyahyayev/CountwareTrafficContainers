using Mhd.Framework.Ioc;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Core
{
    public interface IUnitOfWork : IScopedDependency
    {
        T GetRepository<T>() where T : IRepository;
        int Commit();
        Task<int> CommitAsync();
    }
}
