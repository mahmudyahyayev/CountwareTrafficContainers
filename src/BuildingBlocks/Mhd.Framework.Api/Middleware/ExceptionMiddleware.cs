using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Core;
using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mhd.Framework.Api
{
    internal class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try { await _next(httpContext); }
            catch (Exception e) { await HandleExceptionAsync(httpContext, e); }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ErrorModel errorModel;

            if (ex is not BaseException baseException)
            {
                int statusCode;

                statusCode = ex switch
                {
                    ArgumentNullException or
                    ArgumentOutOfRangeException or
                    DivideByZeroException or
                    IndexOutOfRangeException or
                    InvalidCastException or
                    NullReferenceException or
                    OutOfMemoryException => (int)HttpStatusCode.InternalServerError,
                    TimeoutException => (int)HttpStatusCode.RequestTimeout,

                    _ => (int)HttpStatusCode.BadRequest,
                };

                errorModel = new ErrorModel(new ErrorResult[] { new(ex.Message) }, statusCode, ResponseMessageType.UnhandledException);
            }
            else
                errorModel = baseException.ErrorModel;

            return HandleAsync(httpContext, errorModel);
        }

        private Task HandleAsync(HttpContext context, ErrorModel baseApiErrorResponse)
        {
            context.Response.ContentType = "application/json";
            var options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault };
            var json = JsonSerializer.Serialize(baseApiErrorResponse, options);
            context.Response.StatusCode = baseApiErrorResponse.Code;
            context.Response.Headers.Add("RequestId", context.TraceIdentifier);

            return context.Response.WriteAsync(json);
        }
    }
}
