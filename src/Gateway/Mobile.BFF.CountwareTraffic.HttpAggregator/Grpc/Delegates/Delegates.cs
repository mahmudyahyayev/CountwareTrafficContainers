using Grpc.Core;
using System;
using System.Threading;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <param name="deadline"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public delegate AsyncUnaryCall<TResponse> UnaryCallgRPCServiceMethodHandler<TRequest, TResponse>(TRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
        where TRequest : class
        where TResponse : class;
}
