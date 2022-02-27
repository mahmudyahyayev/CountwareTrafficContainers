using Microsoft.AspNetCore.Builder;

namespace Sensormatic.Tool.Api
{
    public static class Extensions
    {
        public static IApplicationBuilder UseSensormaticExceptionMiddleware(this IApplicationBuilder builder) 
            => builder.UseMiddleware<ExceptionMiddleware>();
    }
}
