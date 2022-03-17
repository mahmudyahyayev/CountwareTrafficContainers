using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mhd.Framework.Grpc.Server
{
    internal class TracingAsyncStreamReader<TRequest> : IAsyncStreamReader<TRequest> where TRequest :class
    {
        private readonly IAsyncStreamReader<TRequest> _innerReader;
        private readonly Func<TRequest, Task> _expression;

        public TracingAsyncStreamReader(IAsyncStreamReader<TRequest> innerReader, Func<TRequest, Task> expression)
        {
            _innerReader = innerReader;
            _expression = expression;
        }

        public async Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            var hasNext = await _innerReader.MoveNext(cancellationToken).ConfigureAwait(false);

            if (hasNext)
                await _expression.Invoke(Current).ConfigureAwait(false);

            return hasNext;
        }

        public TRequest Current => _innerReader.Current;
    }
}
