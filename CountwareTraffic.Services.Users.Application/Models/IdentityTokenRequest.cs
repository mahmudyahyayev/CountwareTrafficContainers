namespace CountwareTraffic.Services.Users.Application
{
    public class IdentityTokenRequest
    {
        public IdentityTokenRequest()
        {
            UserIdentifierType = IdentityUserIdentifierType.UserName;
        }

        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public IdentityUserIdentifierType UserIdentifierType { get; set; }
    }
}
