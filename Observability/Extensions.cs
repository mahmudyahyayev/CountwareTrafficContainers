using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http.Headers;

namespace Countware.Traffic.CrossCC.Observability
{
    public static class Extensions
    {
        private static string CorrelationIdKey = "correlation-id";

        public static IApplicationBuilder UserCorrelationId(this IApplicationBuilder app)
            => app.Use(async (ctx, next) =>
            {
                if (!ctx.Request.Headers.TryGetValue(CorrelationIdKey, out var correlationId))
                    correlationId = Guid.NewGuid().ToString("N");

                ctx.Items[CorrelationIdKey] = correlationId.ToString();

                await next();
            });

        public static string GetCorrelationId(this HttpContext httpContext)
            => httpContext.Items.TryGetValue(CorrelationIdKey, out var correlationId) ? correlationId as string : null;

        public static string GetCorrelationId(this ServerCallContext context)
            => GetCorrelationId(context.GetHttpContext());

        public static void AddCorrelationId(this HttpRequestHeaders headers, string correlationId)
            => headers.TryAddWithoutValidation(CorrelationIdKey, correlationId);

        public static void AddCorrelationId(this Metadata metadata, string correlationId)
            => metadata.Add(CorrelationIdKey, correlationId);

        public static void AddCorrelationId(this Metadata metadata, HttpContext httpContext)
            => metadata.Add(CorrelationIdKey, httpContext.GetCorrelationId());

    }
}
