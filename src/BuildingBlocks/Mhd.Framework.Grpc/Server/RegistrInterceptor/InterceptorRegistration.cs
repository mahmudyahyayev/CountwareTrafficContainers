using Grpc.AspNetCore.Server;

namespace Mhd.Framework.Grpc.Server
{
    public interface IInterceptorRegistration
    {
        void InstallInterceptors(GrpcServiceOptions options);
    }

    public class InterceptorRegistration : IInterceptorRegistration
    {
        public void InstallInterceptors(GrpcServiceOptions options)
        {
            options.Interceptors.Add<MonitoringInterceptor>();
            options.Interceptors.Add<ValidationInterceptor>();
            options.Interceptors.Add<ExceptionInterceptor>();
        }
    }
}
