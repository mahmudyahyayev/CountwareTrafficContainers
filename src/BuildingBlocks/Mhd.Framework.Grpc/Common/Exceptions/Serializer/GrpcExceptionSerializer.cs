using Newtonsoft.Json;
using Mhd.Framework.Core;
using System;
using System.IO;

namespace Mhd.Framework.Grpc.Common
{
    public interface IGrpcExceptionSerializer
    {
        void Serialize(Exception exception, Stream stream);
        Exception Deserialize(Stream stream);
    }

    public class GrpcExceptionSerializer : IGrpcExceptionSerializer
    {
        public Exception Deserialize(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(stream);
            var serialized = streamReader.ReadToEnd();
            var errorModel = JsonConvert.DeserializeObject<ErrorModel>(serialized);
            return new GrpcException(errorModel.ErrorResults, errorModel.Code, Enum.Parse<ResponseMessageType>(errorModel.Type));
        }

        public void Serialize(Exception exception, Stream stream)
        {
            if (exception is GrpcException gRPCException)
            {
                string serialized = JsonConvert.SerializeObject(gRPCException.ErrorModel);
                var streamWriter = new StreamWriter(stream);
                streamWriter.Write(serialized);
                streamWriter.Flush();
            }
        }
    }
}
