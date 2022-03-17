using Convey.CQRS.Queries;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Identity.Application
{
    public class GetTokenHandler : IQueryHandler<GetToken, IdentityTokenResponse>
    {
        private readonly IIdentityService _identityService;

        public GetTokenHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<IdentityTokenResponse> HandleAsync(GetToken query)
        {
            var identifierType = IdentityUserIdentifierType.UserName;

            if (query.GrantType == "password")
            {
                if (IsValidMobilePhone(query.Username))
                    identifierType = IdentityUserIdentifierType.PhoneNumber;
            }

            return await _identityService.GetTokenAsync(new IdentityTokenRequest
            {
                ClientId = query.ClientId,
                ClientSecret = query.ClientSecret,
                GrantType = query.GrantType,
                Password = query.Password,
                Username = query.Username,
                UserIdentifierType = identifierType,
                RefreshToken = query.RefreshToken
            });
        }

        private bool IsValidMobilePhone(string text)
        {
            if (text.Length != 10)
                return false;

            var regexTrMobilePhone = new Regex(@"^[5][0,3,4,5,6][0-9][0-9]{7}$", RegexOptions.IgnoreCase);
            var matchTrMobilePhone = regexTrMobilePhone.Match(text);

            return matchTrMobilePhone.Success;
        }
    }
}
