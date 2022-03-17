
using Mhd.Framework.Grpc.Common;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Mhd.Framework.Core;
using Mhd.Framework.Ioc;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mhd.Framework.Grpc.Server
{
   public class MonitoringInterceptor : Interceptor, IScopedSelfDependency
    {
        private readonly IMonitorLogServer _logManager;
        public MonitoringInterceptor(IMonitorLogServer logManager) => _logManager = logManager;

        public void Dispose() => GC.SuppressFinalize(this);

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
                                                                                      ServerCallContext context,
                                                                                      UnaryServerMethod<TRequest, TResponse> continuation)
        {
            await TryCreateLogAsync(request, context, MethodType.Unary);
            return await continuation(request, context);
        }

        public override async Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request,
                                                                                     IServerStreamWriter<TResponse> responseStream,
                                                                                     ServerCallContext context,
                                                                                     ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            await TryCreateLogAsync(request, context, MethodType.ServerStreaming);
            await continuation(request, responseStream, context);
        }


        public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream,
                                                                                     ServerCallContext context,
                                                                                     ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            var asyncStreamReader = new TracingAsyncStreamReader<TRequest>(requestStream, request => TryCreateLogAsync(request, context, MethodType.ClientStreaming));
            return await continuation(asyncStreamReader, context);
        }


        public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, 
                                                                                     IServerStreamWriter<TResponse> responseStream, 
                                                                                     ServerCallContext context, 
                                                                                     DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            var asyncStreamReader = new TracingAsyncStreamReader<TRequest>(requestStream, request => TryCreateLogAsync(request, context, MethodType.DuplexStreaming));
            await continuation(asyncStreamReader, responseStream, context);
        }


        #region private methods

        private bool first = true;

        private new async Task TryCreateLogAsync<TRequest>(TRequest request, ServerCallContext context, MethodType methodType) where TRequest : class
        { 
            var methodName = context.Method.Split('/').Last();

            var hasServiceLog = typeof(TRequest).CustomAttributes.Any(item => item.AttributeType == typeof(ServiceLogAttribute));

            if (first)
            {
                context.GetHttpContext().Items.Add("HasServiceLog", hasServiceLog.ToString());

                if (_logManager.TryCreateLog(methodName, methodType, request, hasServiceLog, out WebServiceLog log))
                {
                    context.GetHttpContext().Items.Add("SessionInformation", log);
                }

                first = false;
            }
            else
            {
                if (hasServiceLog && context.GetHttpContext().Items["SessionInformation"] is WebServiceLog log)
                    log.ServiceLog.Requests.Add(new Request { Id = Guid.NewGuid(), Data = request });
            }
        }

        private bool HasServiceLog(Type serviceType, string methodName, Type[] inputParameterTypes)
        {
            MethodInfo methodInfo = serviceType.GetMethod(methodName, inputParameterTypes);

            var attribute = methodInfo?.GetCustomAttributes(typeof(ServiceLogAttribute), true).FirstOrDefault();
            if (attribute == null)
                return false;

            return true;
        }

        #endregion private methods
    }
}
