using Grpc.Core;
using System;

namespace Mhd.Framework.Grpc.Client
{
    /// <summary>
    /// Custom delegate handled client streaming (Requests)
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="request"></param>
    /// <param name="methodName"></param>
    /// <param name="methodType"></param>
    public delegate void AsycClientStreamWriterHandler<in TRequest>(TRequest request, string methodName, MethodType methodType) where TRequest :class;


    /// <summary>
    /// Custom delegate handled server streaming exception (Exceptions)
    /// </summary>
    /// <typeparam name="Excption"></typeparam>
    /// <param name="response"></param>
    public delegate void AsycStreamReaderFinishExceptionHandler(Exception Exception);


    /// <summary>
    /// Custom delegate handled server streaming (Responses)
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="response"></param>
    public delegate void AsycStreamReaderSuccessResponseHandler<in TResponse>(TResponse response) where TResponse : class;
}
