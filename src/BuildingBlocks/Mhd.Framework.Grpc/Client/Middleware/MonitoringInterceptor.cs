using Mhd.Framework.Grpc.Common;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Mhd.Framework.Core;
using Mhd.Framework.Ioc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mhd.Framework.Grpc.Client
{ 
    public class MonitoringInterceptor : Interceptor, ITransientSelfDependency
    {
        private readonly IMonitorLogClient _logManager;
        private WebServiceLog log;
        private bool isFirst = true;

        public MonitoringInterceptor(IMonitorLogClient logManager) => _logManager = logManager;

        public void Dispose() => GC.SuppressFinalize(this);


        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            if (bool.TryParse(context.Options.Headers?
                .FirstOrDefault(h => h.Key.Equals("HasClientsideLog", StringComparison.OrdinalIgnoreCase))?.Value, out bool hasClientsideLog) && hasClientsideLog)
            {
                var call = continuation(request, context);

                return new AsyncUnaryCall<TResponse>(TryUnaryLogAsync(call.ResponseAsync, request, context, MethodType.Unary), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
            }

            return base.AsyncUnaryCall(request, context, continuation);
        }


        public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            if (bool.TryParse(context.Options.Headers?
                .FirstOrDefault(h => h.Key.Equals("HasClientsideLog", StringComparison.OrdinalIgnoreCase))?.Value, out bool hasClientsideLog) && hasClientsideLog)
            {
                var call = continuation(context);

                var tracingRequestStream = new TracingClientStreamWriter<TRequest>(call.RequestStream, context.Method.Name, MethodType.ClientStreaming, HandleRequest, null);

                var responseAsync = call.ResponseAsync.ContinueWith(responseTask =>
                {
                    try
                    {
                        var response = responseTask.Result;
                        HandleSuccessUniqueResponse(response);
                        return response;
                    }
                    catch (Exception ex)
                    {
                        if (GrpcExceptionHandler.TryGetErrorModel(ex, out ErrorModel errorModel))
                        {
                            if (_logManager.TryClientSideAddResponseLog(errorModel, log))
                                _logManager.CompleteClientSideLog(log);
                        }
                        throw;
                    }
                });

                var responseHeaderAsync = call.ResponseHeadersAsync.ContinueWith(taskMetadata => taskMetadata.Result);

                return new AsyncClientStreamingCall<TRequest, TResponse>(tracingRequestStream, responseAsync, responseHeaderAsync, call.GetStatus, call.GetTrailers, call.Dispose);
            }

            return base.AsyncClientStreamingCall(context, continuation);
        }


        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            if (bool.TryParse(context.Options.Headers?
                .FirstOrDefault(h => h.Key.Equals("HasClientsideLog", StringComparison.OrdinalIgnoreCase))?.Value, out bool hasClientsideLog) && hasClientsideLog)
            {
                throw new NotSupportedException("We haven't tested yet! Mahmud Yahyayev");

                HandleRequest(request, context.Method.Name, MethodType.ServerStreaming);

                var call = continuation(request, context);

                var streamActions = new TracingAsyncStreamReader<TResponse>.StreamActions(
                    response => _logManager.TryClientSideAddResponseLog(response, log),
                    () => _logManager.CompleteClientSideLog(log),
                    exception => { 
                                    if (GrpcExceptionHandler.TryGetErrorModel(exception, out ErrorModel errorModel))
                                    {
                                        if (_logManager.TryClientSideAddResponseLog(errorModel, log))
                                            _logManager.CompleteClientSideLog(log);
                                    }
                                 }
                );

                var tracingResponseStream = new TracingAsyncStreamReader<TResponse>(call.ResponseStream, streamActions);

                var responseHeaderAsync = call.ResponseHeadersAsync.ContinueWith(taskMetadata => taskMetadata.Result);

                return new AsyncServerStreamingCall<TResponse>(tracingResponseStream, responseHeaderAsync, call.GetStatus, call.GetTrailers, call.Dispose);
            }

            return base.AsyncServerStreamingCall(request, context, continuation);
        }


        //todo: 
        public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            if (bool.TryParse(context.Options.Headers?
                .FirstOrDefault(h => h.Key.Equals("HasClientsideLog", StringComparison.OrdinalIgnoreCase))?.Value, out bool hasClientsideLog) && hasClientsideLog)
            {
                throw new NotSupportedException("We haven't tested yet! Mahmud Yahyayev");
            }

            return base.AsyncDuplexStreamingCall(context, continuation);
        }




        #region private methods
        private void HandleSuccessUniqueResponse<TResponse>(TResponse response) where TResponse : class
        {
            if (_logManager.TryClientSideAddResponseLog(response, log))
                _logManager.CompleteClientSideLog(log);
        }

        private async Task<TResponse> TryUnaryLogAsync<TRequest, TResponse>(Task<TResponse> responseAsync, TRequest request, ClientInterceptorContext<TRequest, TResponse> context, MethodType methodType) 
            where TRequest : class
            where TResponse : class
        {
            _logManager.TryCreateLog(context.Method.Name, methodType, request, true, out log);

            try
            {
                var response = await responseAsync;

                HandleSuccessUniqueResponse(response);

                return response;
            }
            catch (Exception ex)
            {
                if (GrpcExceptionHandler.TryGetErrorModel(ex, out ErrorModel errorModel))
                {
                    if (_logManager.TryClientSideAddResponseLog(errorModel, log))
                        _logManager.CompleteClientSideLog(log);
                }

                throw;
            }
        }

        private void HandleRequest<TRequest>(TRequest request, string methodName, MethodType methodType)
            where TRequest : class
        {
            if (isFirst)
            {
                _logManager.TryCreateLog(methodName, methodType, request, true, out log);
                isFirst = false;
            }
            else
                log.ServiceLog.Requests.Add(new Request() { Id = Guid.NewGuid(), Data = request });
        }     
        #endregion private methods
    }
}
