using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Grpc.Common;
using System;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc
{
    public class GrpcLogHandler : ILogProvider
    {
        private readonly ILogger<GrpcLogHandler> _logger;
        public GrpcLogHandler(ILogger<GrpcLogHandler> logger)
        {
            _logger = logger;
        }
        public Task WriteLogAsync(WebServiceLog data) 
            => Task.Run(() => Console.WriteLine(data.ServiceLog.Id));
    }
}
