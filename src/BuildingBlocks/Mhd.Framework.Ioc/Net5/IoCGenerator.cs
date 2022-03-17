using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mhd.Framework.Ioc
{
    public sealed class IoCGenerator
    {
        public class DoTNet
        {
            private static readonly Lazy<DoTNet> lazy = new(() => new DoTNet());
            public static DoTNet Current => lazy.Value;
            private DoTNet()
            {
            }

            private readonly IDictionary<Type, Action<Type, IServiceCollection>> GetLifeCycle =
               new Dictionary<Type, Action<Type, IServiceCollection>> {
                { typeof(ISingletonDependency), (implementationType, service) => implementationType.GetTypeInfo().ImplementedInterfaces.Where(m => m != typeof(ISingletonDependency) && !m.Namespace.Equals("fi.Framework") && !m.Namespace.Contains("System")).ToList().ForEach(i => service.AddSingleton(i, implementationType)) },
                { typeof(IScopedDependency), (implementationType, service) => implementationType.GetTypeInfo().ImplementedInterfaces.Where(m => m != typeof(IScopedDependency) && !m.Namespace.Equals("fi.Framework") && !m.Namespace.Contains("System")).ToList().ForEach(i => service.AddScoped(i, implementationType)) },
                { typeof(ITransientDependency), (implementationType, service) => implementationType.GetTypeInfo().ImplementedInterfaces.Where(m => m != typeof(ITransientDependency) && !m.Namespace.Equals("fi.Framework") && !m.Namespace.Contains("System")).ToList().ForEach(i => service.AddTransient(i, implementationType)) },
                { typeof(IScopedSelfDependency), (implementationType, service) => service.AddScoped(implementationType) },
                { typeof(ISingletonSelfDependency), (implementationType, service) => service.AddSingleton(implementationType) },
                { typeof(ITransientSelfDependency), (implementationType, service) => service.AddTransient(implementationType) }
                };

            internal IServiceCollection Services { get; private set; }
            internal IConfiguration Configuration { get; private set; }

            public void Start(IServiceCollection services, IConfiguration configuration)
            {
                Services = services;
                Configuration = configuration;
                RegisterInterfaceBasedTypes();
                ConfigureOptionsCore();
            }

            private void RegisterInterfaceBasedTypes()
            {
                var dependencyObjects = AppDomain.Current.GetAllAssemblies().SelectMany(s => s.DefinedTypes.Where(w => !w.IsAbstract && w.IsClass).Select(sm => sm));

                var pureObjects = dependencyObjects.Where(w => (typeof(ITransientDependency).IsAssignableFrom(w)
                                            || typeof(ISingletonDependency).IsAssignableFrom(w)
                                            || typeof(IScopedDependency).IsAssignableFrom(w)
                                            || typeof(ISingletonSelfDependency).IsAssignableFrom(w)
                                            || typeof(ITransientSelfDependency).IsAssignableFrom(w)
                                            || typeof(IScopedSelfDependency).IsAssignableFrom(w))
                                        && !w.IsInterface
                                        && !w.IsAbstract)
                                  .Select(x => new TypeOfLifeCycle(x));

                if (!pureObjects.Any())
                    return;

                foreach (var dependencyObject in pureObjects)
                {
                    if (GetLifeCycle.TryGetValue(dependencyObject.LifeCycle, out Action<Type, IServiceCollection> _method))
                        _method.Invoke(dependencyObject.ImplementationType, Services);
                    else
                        throw new ArgumentNullException($"LifeCycle : {dependencyObject.LifeCycle.FullName} \r\n ImplementationType: {dependencyObject.ImplementationType.FullName}", "Ioc yükleme esnasında hata oluşmuştur.");
                }
            }


            private void ConfigureOptionsCore()
            {
                var optionTypes = new List<Type>();
                foreach (var item in AppDomain.Current.GetAllAssemblies())
                {
                    optionTypes.AddRange(item.ExportedTypes);
                }
                optionTypes = optionTypes.Where(x => x.IsClass && typeof(IConfigurationOptions).IsAssignableFrom(x)).ToList();
                foreach (var options in optionTypes)
                {
                    typeof(OptionsConfigurationServiceCollectionExtensions).GetMethods().First()
                        .MakeGenericMethod(options)
                        .Invoke(Configuration, new object[] { Services, Configuration.GetSection(options.Name) });
                }
                foreach (var type in optionTypes)
                {
                    Services.AddSingleton(type, resolver =>
                    {
                        var optionType = typeof(IOptions<>).MakeGenericType(type);
                        return resolver.GetService(optionType).GetValue(nameof(IOptions<dynamic>.Value));
                    });
                }
            }
        }
    }
}
