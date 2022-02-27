using Grpc.Core;
using Sensormatic.Tool.Core;
using Sensormatic.Tool.Grpc.Common;
using System;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc
{
    public static class Extension
    {
        public static void ThrowClientgRPCException(this ErrorModel model)
        => throw new ClientGrpcException(model.ErrorResults, (int)StatusCodeConverter.ConvertToHttpStatusCode((StatusCode)model.Code), Enum.Parse<ResponseMessageType>(model.Type));

        public static ClientGrpcException CreateClientgRPCException(this ErrorModel model)
            => new ClientGrpcException(model.ErrorResults, (int)StatusCodeConverter.ConvertToHttpStatusCode((StatusCode)model.Code), Enum.Parse<ResponseMessageType>(model.Type));
    }
}
