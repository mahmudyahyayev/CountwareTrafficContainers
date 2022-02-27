using Convey.CQRS.Queries;

namespace CountwareTraffic.Services.Users.Application
{
    public class GetToken : IQuery<IdentityTokenResponse>
    {
        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
    }
}
