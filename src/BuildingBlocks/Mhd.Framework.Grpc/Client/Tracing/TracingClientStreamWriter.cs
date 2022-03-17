using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace Mhd.Framework.Grpc.Client
{    internal class TracingClientStreamWriter<TRequest> : IClientStreamWriter<TRequest> where TRequest :class
    {
        private readonly IClientStreamWriter<TRequest> _innerWriter;
        private readonly AsycClientStreamWriterHandler<TRequest> _onWrite;
        private readonly Action _onComplete;
        private readonly MethodType _methodType;
        private readonly string _methodName;

        public TracingClientStreamWriter(IClientStreamWriter<TRequest> writer, string methodName, MethodType methodType, AsycClientStreamWriterHandler<TRequest> onWrite, Action onComplete = null)
        {
            _innerWriter = writer;
            _methodType = methodType;
            _methodName = methodName;
            _onWrite = onWrite;
            _onComplete = onComplete;
        }

        public WriteOptions WriteOptions { get => _innerWriter.WriteOptions; set => _innerWriter.WriteOptions = value; }

        public Task WriteAsync(TRequest message)
        {
            _onWrite(message, _methodName, _methodType);
            return _innerWriter.WriteAsync(message);
        }

        public Task CompleteAsync()
        {
            _onComplete?.Invoke();
            return _innerWriter.CompleteAsync();
        }
    }
}
