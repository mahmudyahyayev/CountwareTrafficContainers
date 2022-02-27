using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Sensormatic.Tool.Ioc;
using System;
using System.Linq;
using System.Security.Claims;

namespace CountwareTraffic.WorkerServices.SignalrHub.Application
{
    public class IdentityService : ITransientSelfDependency
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public Guid UserId
        {
            get
            {
                var userId = Guid.Empty;

                if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var subject = _httpContextAccessor.HttpContext.User.Claims
                                                                  .Where(x => x.Type == ClaimTypes.NameIdentifier)
                                                                  .Select(x => x.Value)
                                                                  .FirstOrDefault();

                    var hasUserId = Guid.TryParse(subject, out userId);
                }

                if (userId == default)
                    throw new ApplicationUserIdCouldNotFoundException();

                return userId;
            }
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
