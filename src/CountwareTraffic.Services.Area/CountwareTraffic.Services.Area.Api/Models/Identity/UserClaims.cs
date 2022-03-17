using System;

namespace CountwareTraffic.Services.Areas.Api
{
    public class UserClaims
    {
        public string UniqueName { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public DateTime Birthdate { get; set; }
        public int Gender { get; set; }
        public Guid UserId { get; set; } //gelmesi lazim
    }
}
