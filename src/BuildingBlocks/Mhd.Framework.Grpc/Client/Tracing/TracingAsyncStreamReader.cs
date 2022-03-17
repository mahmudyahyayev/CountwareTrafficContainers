using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mhd.Framework.Grpc.Client
{
    internal partial class TracingAsyncStreamReader<TResponse> : IAsyncStreamReader<TResponse> where TResponse :class
    {
        private readonly IAsyncStreamReader<TResponse> _innerReader;
        private readonly StreamActions _streamActions;

        public TResponse Current => _innerReader.Current;

        public TracingAsyncStreamReader(IAsyncStreamReader<TResponse> innerReader, StreamActions streamActions)
        {
            _innerReader = innerReader;
            _streamActions = streamActions;
        }

        public async Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            try
            {
                var hasNext = await _innerReader.MoveNext(cancellationToken).ConfigureAwait(false);

                if (hasNext)
                    _streamActions.Message(Current);
                else
                    _streamActions.StreamEnd();

                return hasNext;
            }
            catch (Exception ex) { _streamActions.Exception(ex); return false; }
        }

        internal readonly struct StreamActions
        {
            public AsycStreamReaderSuccessResponseHandler<TResponse> OnMessage { get; }
            public AsycStreamReaderFinishExceptionHandler OnException { get; }
            public Action OnStreamEnd { get; }
           
            public StreamActions(AsycStreamReaderSuccessResponseHandler<TResponse> onMessage, Action onStreamEnd = null, AsycStreamReaderFinishExceptionHandler onException = null)
            {
                OnMessage = onMessage;
                OnStreamEnd = onStreamEnd;
                OnException = onException;
            }

            public void Message(TResponse response)=> OnMessage?.Invoke(response);
            public void Exception(Exception ex) => OnException?.Invoke(ex);
            public void StreamEnd() => OnStreamEnd?.Invoke();
        }
    }
}