namespace Mhd.Framework.Grpc.Client
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IInterceptorRegistration
    {
        void InstallInterceptors(IHttpClientBuilder builder);
    }

    public class InterceptorRegistration : IInterceptorRegistration
    {
        public void InstallInterceptors(IHttpClientBuilder builder) => builder.AddInterceptor<MonitoringInterceptor>();
    }
}
