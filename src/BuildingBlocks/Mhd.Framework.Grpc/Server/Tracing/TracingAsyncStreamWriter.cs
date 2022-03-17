using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace Mhd.Framework.Grpc.Server
{
    internal class TracingAsyncStreamWriter<TResponse> : IServerStreamWriter<TResponse> where TResponse : class
    {
        private readonly IAsyncStreamWriter<TResponse> _innerWriter;
        private readonly Func<TResponse, Task> _expression;

        public TracingAsyncStreamWriter(IAsyncStreamWriter<TResponse> innerWriter, Func<TResponse, Task> expression)
        {
            _innerWriter = innerWriter;
            _expression = expression;
        }

        public WriteOptions WriteOptions { get => _innerWriter.WriteOptions; set => _innerWriter.WriteOptions = value; }
        public async Task WriteAsync(TResponse message) => await _expression.Invoke(message).ConfigureAwait(false);
    }
}
