using System;
using System.Threading;
using System.Threading.Tasks;
using Countware.Traffic.Observability;
using Grpc.Core;
using Mhd.Framework.Core;
using Mhd.Framework.Grpc.Common;
using Mhd.Framework.Ioc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc
{
    public class AsyncUnaryCallHandler : ITransientSelfDependency
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static string HasClientsideLog = "HasClientsideLog";
        private static string Authorization = "Authorization";

        public AsyncUnaryCallHandler(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public async Task<TResponse> CallMethodAsync<TRequest, TResponse>(UnaryCallgRPCServiceMethodHandler<TRequest, TResponse> asyncUnaryCallHandler, TRequest request, Metadata metadata = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken), bool hasClientSideLog = false)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                if (metadata == null) metadata = new Metadata();

                if (hasClientSideLog) metadata.Add(HasClientsideLog, "true");

                string token = _httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;

                metadata.Add(Authorization, $"Bearer {token}");

                metadata.AddCorrelationId(_httpContextAccessor.HttpContext);

                return await asyncUnaryCallHandler?
                   .Invoke(request, metadata, deadline, cancellationToken).ResponseAsync;
            }
            catch (Exception ex)
            {
                if (GrpcExceptionHandler.TryGetErrorModel(ex, out ErrorModel model))
                    model.ThrowClientGrpcxception();

                throw;
            }
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
