using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mhd.Framework.Efcore
{
    //Deneme aamaclidir. Base de kullanilmiyor
    public class SaveChangesInterceptor : ISaveChangesInterceptor, IDisposable
    {
        private EntityEntry[] entityEntries;
        public InterceptorDefination InterceptorDefination { get; }

        public IDictionary<int, IDictionary<Type, IInterceptorGenerator>> ContuniesInterceptor;
        public SaveChangesInterceptor(InterceptorDefination interceptorDefination)
        {
            if (interceptorDefination is not null)
                InterceptorDefination = interceptorDefination;

            ContuniesInterceptor = new Dictionary<int, IDictionary<Type, IInterceptorGenerator>>();
        }

        public void SaveChangesFailed(DbContextErrorEventData eventData)
        {
            InterceptError(eventData.Exception);
        }

        public Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            InterceptError(eventData.Exception);
            return Task.CompletedTask;
        }

        public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            InterceptAfter();
            return result;
        }

        public async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            InterceptAfter();
            return await Task.FromResult(result);
        }


        public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            entityEntries = eventData.Context.ChangeTracker.Entries().Where(w => w.State != EntityState.Detached && w.State != EntityState.Unchanged).ToArray();
            InterceptBefore(eventData.Context);

            foreach (var entry in entityEntries.Where(w => w.State is EntityState.Deleted))
                entry.State = EntityState.Modified;

            return result;
        }

        public async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                entityEntries = eventData.Context.ChangeTracker.Entries().Where(w => w.State != EntityState.Detached && w.State != EntityState.Unchanged).ToArray();
                InterceptBefore(eventData.Context);
                foreach (var entry in entityEntries.Where(w => w.State is EntityState.Deleted))
                    entry.State = EntityState.Modified;
            }
            return await Task.FromResult(result);
        }

        void InterceptBefore(DbContext context) 
        {
            foreach (var entry in entityEntries)
            {
                var interceptorGenerators = new Dictionary<Type, IInterceptorGenerator>();

                foreach (Type intface in entry.Entity.GetType().GetInterfaces().Where(w => w.GetInterfaces().Any(a => a == typeof(Mhd.Framework.Efcore.IInterceptor))))
                {
                    if (InterceptorDefination.Interceptors.TryGetValue(intface, out Func<IInterceptorGenerator> p))
                    {
                        var generator = p();
                        interceptorGenerators.Add(intface, generator);
                        generator.OnBefore(entry, context);
                    }
                }
                ContuniesInterceptor.Remove(entry.Entity.GetHashCode());
                ContuniesInterceptor.Add(entry.Entity.GetHashCode(), interceptorGenerators);
            }
        }

        void InterceptError(Exception exception)
        {
            ICollection<Type> includesTypes = new List<Type>();

            foreach (var entry in entityEntries)
            {
                foreach (Type intface in entry.Entity.GetType().GetInterfaces().Where(w => w.GetInterfaces().Any(a => a == typeof(Mhd.Framework.Efcore.IInterceptor))))
                {
                    if (ContuniesInterceptor.TryGetValue(entry.Entity.GetHashCode(), out IDictionary<Type, IInterceptorGenerator> generators))
                        if (generators.TryGetValue(intface, out IInterceptorGenerator interceptorGenerator) && !includesTypes.Contains(intface))
                        {
                            interceptorGenerator.OnError(exception);
                            includesTypes.Add(intface);
                        }
                }
            }
        }

        void InterceptAfter()
        {
            ICollection<Type> includesTypes = new List<Type>();

            foreach (var entry in entityEntries)
            {
                foreach (Type intface in entry.Entity.GetType().GetInterfaces().Where(w => w.GetInterfaces().Any(a => a == typeof(Mhd.Framework.Efcore.IInterceptor))))
                {
                    if (ContuniesInterceptor.TryGetValue(entry.Entity.GetHashCode(), out IDictionary<Type, IInterceptorGenerator> generators))
                        if (generators.TryGetValue(intface, out IInterceptorGenerator interceptorGenerator) && !includesTypes.Contains(intface))
                        {
                            interceptorGenerator.OnAfter();
                            includesTypes.Add(intface);
                        }
                }
            }
        }

        public void Dispose()
        {
            entityEntries = null;
            ContuniesInterceptor = null;
            GC.SuppressFinalize(this);
        }
        ~SaveChangesInterceptor() => Dispose();
    }
}
