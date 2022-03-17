using Microsoft.AspNetCore.Builder;

namespace Mhd.Framework.Api
{
    public static class Extensions
    {
        public static IApplicationBuilder UseSensormaticExceptionMiddleware(this IApplicationBuilder builder) 
            => builder.UseMiddleware<ExceptionMiddleware>();
    }
}
