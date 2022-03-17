using CountwareTraffic.Services.Events.Application;
using Microsoft.Extensions.Options;
using System;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class IdentityService : IIdentityService
    {
        private readonly IOptions<ApplicationUser> _applicationUser;
        public IdentityService(IOptions<ApplicationUser> applicationUser) => _applicationUser = applicationUser;

        public Guid UserId
        {
            get
            {
                var applicationUserId = _applicationUser.Value.Id;

                if (applicationUserId == default)
                    throw new ApplicationUserIdCouldNotFoundException();

                return applicationUserId;
            }
        }

        public void Dispose()
            => GC.SuppressFinalize(this);
    }
}
