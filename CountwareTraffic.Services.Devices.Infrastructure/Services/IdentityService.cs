using CountwareTraffic.Services.Devices.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<ApplicationUser> _applicationUser;

        public IdentityService(IHttpContextAccessor httpContextAccessor, IOptions<ApplicationUser> applicationUser)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationUser = applicationUser;
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
                    userId = _applicationUser.Value.Id;


                if (userId == default)
                    throw new ApplicationUserIdCouldNotFoundException();

                return userId;
            }
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
