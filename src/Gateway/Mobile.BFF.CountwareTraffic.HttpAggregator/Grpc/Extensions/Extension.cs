using Grpc.Core;
using Mhd.Framework.Core;
using Mhd.Framework.Grpc.Common;
using System;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc
{
    public static class Extension
    {
        public static void ThrowClientGrpcxception(this ErrorModel model)
            => throw new ClientGrpcException(model.ErrorResults, (int)StatusCodeConverter.ConvertToHttpStatusCode((StatusCode)model.Code), Enum.Parse<ResponseMessageType>(model.Type));

        public static ClientGrpcException CreateClientGrpcException(this ErrorModel model)
            => new ClientGrpcException(model.ErrorResults, (int)StatusCodeConverter.ConvertToHttpStatusCode((StatusCode)model.Code), Enum.Parse<ResponseMessageType>(model.Type));
    }
}
