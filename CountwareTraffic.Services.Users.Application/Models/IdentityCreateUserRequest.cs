using System.Collections.Generic;

namespace CountwareTraffic.Services.Users.Application
{
    public class IdentityCreateUserRequest
    {
        public IdentityCreateUserRequest()
        {
            Roles = new List<string>();
            Claims = new List<IdentityClaimRequest>();
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string CitizenshipId { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual bool PhoneNumberConfirmed { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<IdentityClaimRequest> Claims { get; set; }
        public List<string> Roles { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
