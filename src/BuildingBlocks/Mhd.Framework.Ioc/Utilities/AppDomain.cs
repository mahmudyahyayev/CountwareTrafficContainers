using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Mhd.Framework.Ioc
{
    internal class AppDomain
    {
        private static readonly Lazy<AppDomain> lazy = new(() => new AppDomain());
        public static AppDomain Current => lazy.Value;
        private ICollection<Assembly> assemblies;
        private AppDomain() { assemblies = new HashSet<Assembly>(); }

        public ICollection<Assembly> GetAllAssemblies()
        {
            if (assemblies.Any()) return assemblies;

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            string[] excludePrefix = { "Microsoft.", "System." };

            var files = Directory.GetFiles(path, "*.dll").Where(i => !excludePrefix.Any(j => i.Contains(j)));

            foreach (var dll in files)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dll);
                    assemblies.Add(assembly);
                }
                catch { }
            }

            return assemblies;
        }
    }

    internal class TypeOfLifeCycle
    {
        public TypeOfLifeCycle(Type implementationType)
        {
            ImplementationType = implementationType;
        }
        public Type ImplementationType { get; }
        public Type LifeCycle
            => typeof(ITransientDependency).IsAssignableFrom(ImplementationType)
                                                    ? typeof(ITransientDependency)
                                                    : typeof(IScopedDependency).IsAssignableFrom(ImplementationType)
                                                        ? typeof(IScopedDependency)
                                                        : typeof(ISingletonDependency).IsAssignableFrom(ImplementationType)
                                                            ? typeof(ISingletonDependency)
                                                            : typeof(IScopedSelfDependency).IsAssignableFrom(ImplementationType)
                                                                ? typeof(IScopedSelfDependency)
                                                                : typeof(ISingletonSelfDependency).IsAssignableFrom(ImplementationType)
                                                                    ? typeof(ISingletonSelfDependency)
                                                                    : typeof(ITransientSelfDependency).IsAssignableFrom(ImplementationType)
                                                                        ? typeof(ITransientSelfDependency)
                                                                        : null;
    }

    internal static class ObjectExtensions
    {
        internal static object GetValue(this object obj, string propertyNameToGetValueFrom)
            => obj.GetType().GetProperty(propertyNameToGetValueFrom)?.GetValue(obj, null);
    }
}
