using Grpc.Core;
using Newtonsoft.Json;
using Mhd.Framework.Core;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Mhd.Framework.Grpc.Common
{
    public static class GrpcExceptionHandler
    {
        public const string GrpcMetaDataKeyError = "errors-text";

        public static bool SetException(Metadata metadata, Exception exception)
        {
            Type actualType = exception.GetType();

            IGrpcExceptionSerializer exceptionSerializer = GetSerializer(actualType);

            if (exceptionSerializer != null)
            {
                MemoryStream memoryStream = new();
                exceptionSerializer.Serialize(exception, memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                byte[] byteContent = memoryStream.ToArray();

                var exceptionContent = new GRPCExceptionMeta(actualType, byteContent);
                string serializedExceptionContent = JsonConvert.SerializeObject(exceptionContent);
                metadata.Add(new Metadata.Entry(GrpcMetaDataKeyError, serializedExceptionContent));

                return true;
            }

            return false;
        }

        public static bool TryGetErrorModel(Exception exception, out ErrorModel errorModel)
        {
            if (exception is RpcException rpcException)
            {
                if (rpcException.Trailers != null && string.Equals(rpcException.Status.Detail, GrpcMetaDataKeyError, StringComparison.OrdinalIgnoreCase))
                {
                    return TryGetErrorModel(rpcException.Trailers, out errorModel);
                }
            }
            else
            {
                if (exception.InnerException != null) { return TryGetErrorModel(exception.InnerException, out errorModel); }
            }

            errorModel = null;

            return false;
        }

        private static IGrpcExceptionSerializer GetSerializer(Type actualType)
        {
            if (actualType.GetCustomAttribute<GrpcExceptionSerializerAttribute>() is GrpcExceptionSerializerAttribute serializer)
                return (IGrpcExceptionSerializer)Activator.CreateInstance(serializer.SerializerType);

            return null;
        }

        private static bool TryGetErrorModel(Metadata metadata, out ErrorModel errorModel)
        {
            if (metadata.Any(x => string.Equals(x.Key, GrpcMetaDataKeyError, StringComparison.OrdinalIgnoreCase)))
            {
                string serializedExceptionContent = metadata.SingleOrDefault(x => string.Equals(x.Key, GrpcMetaDataKeyError, StringComparison.OrdinalIgnoreCase))?.Value;

                var exceptionContent = JsonConvert.DeserializeObject<GRPCExceptionMeta>(serializedExceptionContent);

                var memoryStream = new MemoryStream(exceptionContent.Serialized);

                memoryStream.Seek(0, SeekOrigin.Begin);

                var exceptionSerializer = GetSerializer(exceptionContent.ExceptionType);

                if (exceptionSerializer.Deserialize(memoryStream) is GrpcException exception)
                {
                    errorModel = exception.ErrorModel;
                    return true;
                }
            }

            errorModel = null;
            return false;
        }
    }
}
