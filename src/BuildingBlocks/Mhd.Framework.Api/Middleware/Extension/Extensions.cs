using Microsoft.AspNetCore.Builder;

namespace Mhd.Framework.Api
{
    public static class Extensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder) 
            => builder.UseMiddleware<ExceptionMiddleware>();
    }
}
