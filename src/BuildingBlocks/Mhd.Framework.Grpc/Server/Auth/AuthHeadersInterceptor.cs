using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;

namespace Mhd.Framework.Grpc.Server
{
    public class AuthHeadersInterceptor : Interceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthHeadersInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        //todo: override virtual methods from 'Interceptor' and write something :)
    }
}
