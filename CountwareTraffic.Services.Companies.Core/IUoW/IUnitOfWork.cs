using Sensormatic.Tool.Ioc;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Core
{
    public interface IUnitOfWork : IScopedDependency
    {
        T GetRepository<T>() where T : IRepository;
        int Commit();
        Task<int> CommitAsync();
    }
}
