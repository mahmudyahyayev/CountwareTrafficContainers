using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Mhd.Framework.Grpc.Client
{
    public static class Extension
    {
        public static IHttpClientBuilder RegisterClientInterceptors(this IHttpClientBuilder builder)
        {
            Assembly.GetExecutingAssembly()
               .ExportedTypes
               .Where(x => typeof(IInterceptorRegistration).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
               .Select(Activator.CreateInstance)
               .Cast<IInterceptorRegistration>()
               .ToList().ForEach(interceptor => interceptor.InstallInterceptors(builder));

            return builder;
        }
    }
}
