using Grpc.Core;
using Microsoft.AspNetCore.Http;

namespace Mhd.Framework.Grpc.Common
{
    public static class Extension
    {
        private static string CorrelationIdKey = "correlation-id";

        public static string GetCorrelationId(this ServerCallContext context)
            => GetCorrelationId(context.GetHttpContext());

        public static string GetCorrelationId(this HttpContext httpContext)
            => httpContext.Items.TryGetValue(CorrelationIdKey, out var correlationId) ? correlationId as string : null;
    }
}
