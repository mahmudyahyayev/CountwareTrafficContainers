using System;

namespace Mhd.Framework.Grpc.Common
{
    public class GRPCExceptionMeta
    {
        public Type ExceptionType { get; }
        public byte[] Serialized { get; }
        public GRPCExceptionMeta(Type exceptionType, byte[] serialized)
        {
            ExceptionType = exceptionType;
            Serialized = serialized;
        }
    }
}
