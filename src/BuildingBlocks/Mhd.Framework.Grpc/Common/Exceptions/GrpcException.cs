using Mhd.Framework.Core;
using System.Collections.Generic;

namespace Mhd.Framework.Grpc.Common
{
    [GrpcExceptionSerializer(typeof(GrpcExceptionSerializer))]
    internal class GrpcException : BaseException
    {
        internal GrpcException(ICollection<ErrorResult> errorResults, int code, ResponseMessageType responseMessageType) : base(errorResults, code, responseMessageType)
        {
        }
    }
}
