using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc
{
    public class ClientGrpcException : BaseException
    {
        public ClientGrpcException(ICollection<ErrorResult> errorResults, int statusCode, ResponseMessageType responseMessageType) 
            : base(errorResults, statusCode, responseMessageType) { }
    }
}
