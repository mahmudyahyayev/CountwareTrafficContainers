using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Jil;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CountwareTraffic.Services.Identity.Application
{
    public class IdentityService : IIdentityService
    {
        private readonly ILogger<IdentityService> _logger;
        private readonly IOptions<AuthenticationConfig> _authConfig;
        private static Jil.Options _options = new Jil.Options(serializationNameFormat: SerializationNameFormat.CamelCase);
        private readonly HttpClient _httpClient;
        private readonly IJsonConverter _jsonConverter;
        public IdentityService(ILogger<IdentityService> logger, IOptions<AuthenticationConfig> authConfig, IHttpClientFactory clientFactory, IJsonConverter jsonConverter)
        {
            _logger = logger;
            _authConfig = authConfig;
            _httpClient = clientFactory.CreateClient();
            _jsonConverter = jsonConverter;
        }

        //mevcut Identity Servere gore degismesi lazim Mahmud Yahyayev
        public async Task<IdentityTokenResponse> GetTokenAsync(IdentityTokenRequest identityTokenRequest)
        {
            var root = new Root();
            root.payload = new Payload
            {
                password = identityTokenRequest.Password,
                username = identityTokenRequest.Username
            };

            var requestUrl = _authConfig.Value.Authority;
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            var jsonData = _jsonConverter.Serialize<Root>(root);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                    throw new IdentityServiceCannotBeAccessedException();

                else
                    throw new GetTokenException(identityTokenRequest.Username);
            }

            var responseString = await response.Content.ReadAsStringAsync();

            var result = _jsonConverter.Deserialize<IdentityTokenResponse>(responseString);

            result.RefreshToken = "";
            result.TokenType = "";
            result.Scope = "";

            return result;
        }

        public class Payload
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class Root
        {
            public Payload payload { get; set; }
        }






        //realda olmasi gerekenler
        public async Task<IdentityTokenResponse> GetTokenAsync_New(IdentityTokenRequest identityTokenRequest)
        {
            var requestUrl = _authConfig.Value.Authority + "connect/token";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("grant_type", identityTokenRequest.GrantType));
            keyValues.Add(new KeyValuePair<string, string>("client_id", identityTokenRequest.ClientId));
            keyValues.Add(new KeyValuePair<string, string>("client_secret", identityTokenRequest.ClientSecret));
            keyValues.Add(new KeyValuePair<string, string>("username", identityTokenRequest.Username));
            keyValues.Add(new KeyValuePair<string, string>("password", identityTokenRequest.Password));

            if (identityTokenRequest.GrantType == "refresh_token")
                keyValues.Add(new KeyValuePair<string, string>("refresh_token", identityTokenRequest.RefreshToken));

            if (identityTokenRequest.UserIdentifierType != IdentityUserIdentifierType.UserName)
                keyValues.Add(new KeyValuePair<string, string>("identifier_type", ((int)identityTokenRequest.UserIdentifierType).ToString()));

            var content = new FormUrlEncodedContent(keyValues);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            request.Content = content;

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var rsp = await response.Content.ReadAsStringAsync();
                return Jil.JSON.Deserialize<IdentityTokenResponse>(rsp, _options);
            }

            throw new GetTokenException(identityTokenRequest.Username);
        }


        public Task<IdentityCheckUserResponse> CheckUserAsync(string accessToken, IdentityCheckUserRequest checkRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateUserAsync(string accessToken, IdentityCreateUserRequest identityCreateUserRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(string accessToken, IdentityDeleteUserRequest identityDeleteUserRequest)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateUsernameAsync(string accessToken, string name, string surname)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResetPasswordResponse> InitiateResetPasswordAsync(string accessToken, string identifier, IdentityResetPasswordRequest identityResetPasswordRequest)
        {
            throw new NotImplementedException();
        }

        public Task PatchUserAsync(string accessToken, IdentityPatchUserRequest identityUpdatePersonalInformationRequest)
        {
            throw new NotImplementedException();
        }

        public Task ResetPasswordAsync(string accessToken, string identifier, IdentityResetPasswordChangeRequest identityResetPasswordChangeRequest)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePasswordAsync(string accessToken, IdentityUpdatePasswordRequest identityUpdatePasswordRequest)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserClaimsAsync(string accessToken, Guid userId, IdentityUpdateUserClaimsRequest identityUpdateUserClaimsRequest)
        {
            throw new NotImplementedException();
        }
    }
}
