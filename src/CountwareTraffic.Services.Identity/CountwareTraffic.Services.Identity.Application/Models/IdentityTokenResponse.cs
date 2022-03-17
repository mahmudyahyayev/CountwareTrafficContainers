using Jil;

namespace CountwareTraffic.Services.Identity.Application
{
    public class IdentityTokenResponse
    {

        // [JilDirective(Name = "access_token")]
        [JilDirective(Name = "authToken")]
        public string AccessToken { get; set; }

        [JilDirective(Name = "token_type")]
        public string TokenType { get; set; }

        [JilDirective(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        [JilDirective(Name = "scope")]
        public string Scope { get; set; }

        [JilDirective(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}
