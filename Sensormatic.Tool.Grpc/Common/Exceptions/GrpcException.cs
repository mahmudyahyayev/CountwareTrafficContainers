using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace Sensormatic.Tool.Grpc.Common
{
    [GrpcExceptionSerializer(typeof(GrpcExceptionSerializer))]
    internal class GrpcException : BaseException
    {
        internal GrpcException(ICollection<ErrorResult> errorResults, int code, ResponseMessageType responseMessageType) : base(errorResults, code, responseMessageType)
        {
        }
    }
}
