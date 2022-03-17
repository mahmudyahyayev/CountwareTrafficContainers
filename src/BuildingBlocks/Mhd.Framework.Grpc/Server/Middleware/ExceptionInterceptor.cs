using Mhd.Framework.Grpc.Common;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Mhd.Framework.Core;
using Mhd.Framework.Ioc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Mhd.Framework.Grpc.Server
{
    public class ExceptionInterceptor : Interceptor, IScopedSelfDependency
    {
        public const string GrpcMetaDataKeyError = "errors-text";

        private readonly IMonitorLogServer _logManager;
        public ExceptionInterceptor(IMonitorLogServer logManager) => _logManager = logManager;

        public void Dispose() => GC.SuppressFinalize(this);

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
                                                                                      ServerCallContext context,
                                                                                      UnaryServerMethod<TRequest, TResponse> continuation)
        {
            TResponse response = default;

            try
            {
                response = await continuation(request, context);
                _logManager.TryServerSideAddResponseLog(response, context);
            }

            catch (Exception e) { await HandleExceptionAsync(context, e); }

            _logManager.CompleteServerSideLog(context);

            return response;
        }


        public override async Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request,
                                                                                     IServerStreamWriter<TResponse> responseStream,
                                                                                     ServerCallContext context,
                                                                                     ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                var asyncStreamWriter = new TracingAsyncStreamWriter<TResponse>(responseStream, response => HandleSuccessResponseAsync(response, context));
                await continuation(request, asyncStreamWriter, context);

                _logManager.CompleteServerSideLog(context);
            }

            catch (Exception e) { await HandleExceptionAsync(context, e); }
        }


        public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream,
                                                                                     ServerCallContext context,
                                                                                     ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            TResponse response = default;
            try
            {
                response = await continuation(requestStream, context);
                _logManager.TryServerSideAddResponseLog(response, context);
            }

            catch (Exception e) { await HandleExceptionAsync(context, e); }

            _logManager.CompleteServerSideLog(context);
            return response;
        }

        public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                var asyncStreamWriter = new TracingAsyncStreamWriter<TResponse>(responseStream, response => HandleSuccessResponseAsync(response, context));
                await continuation(requestStream, asyncStreamWriter, context);

                _logManager.CompleteServerSideLog(context);
            }

            catch (Exception e) { await HandleExceptionAsync(context, e); }
        }




        #region private methods

        private new async Task HandleSuccessResponseAsync<TResponse>(TResponse response, ServerCallContext context) where TResponse : class => _logManager.TryServerSideAddResponseLog(response, context);


        /// <summary>
        ///  It handles any exception and turns it into our custom exception (GrpcException derived on BaseException) and throws RpcException.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private new async Task HandleExceptionAsync(ServerCallContext context, Exception ex)
        {
            ErrorModel errorModel;

            if (ex is RpcException) throw ex;

            else if (ex is BaseException baseException) errorModel = baseException.ErrorModel;

            else
            {
                int code;
                code = ex switch
                {
                    ArgumentNullException or
                    ArgumentOutOfRangeException or
                    DivideByZeroException or
                    IndexOutOfRangeException or
                    InvalidCastException or
                    NullReferenceException or
                    OutOfMemoryException => (int)HttpStatusCode.InternalServerError,
                    TimeoutException => (int)HttpStatusCode.RequestTimeout,
                    _ => (int)HttpStatusCode.BadRequest,
                };

                errorModel = new ErrorModel(new ErrorResult[] { new(ex.Message) }, code, ResponseMessageType.Error);
            }


            if (Enum.TryParse(errorModel.Type, out ResponseMessageType responsemessageType))
            {
                var customGrpcException = new GrpcException(errorModel.ErrorResults, errorModel.Code, responsemessageType);

                if (GrpcExceptionHandler.SetException(context.ResponseTrailers, customGrpcException))
                {
                    if (_logManager.TryServerSideAddResponseLog(customGrpcException.ErrorModel, context))
                    {
                        _logManager.CompleteServerSideLog(context);

                        throw new RpcException(
                                                new Status(
                                                           StatusCodeConverter.ConvertToGrpcCode((HttpStatusCode)errorModel.Code),
                                                           GrpcMetaDataKeyError
                                                          ),
                                                new Metadata()
                                               );
                    }
                }
            }
        }

        #endregion private methods
    }
}






