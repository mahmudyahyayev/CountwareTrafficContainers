using CountwareTraffic.Services.Users.Grpc;
using Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc;
using Sensormatic.Tool.Grpc.Client;
using Sensormatic.Tool.Ioc;
using System;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class UserService : IScopedSelfDependency
    {
        private readonly User.UserClient _userClient;
        private readonly AsyncUnaryCallHandler _asyncUnaryCallHandler;

        public UserService(User.UserClient userClient, AsyncUnaryCallHandler asyncUnaryCallHandler)
        {
            _userClient = userClient;
            _asyncUnaryCallHandler = asyncUnaryCallHandler;
        }

        public async Task<TokenResponse> GetTokenAsync(TokenRequest request)
        {
            GetTokenRequest grpcRequest = new()
            {
                ClientId = request.ClientId,
                ClientSecret = request.ClientSecret,
                GrantType = request.GrantType,
                Password = request.Password,
                RefreshToken = request.RefreshToken,
                Username = request.Username
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_userClient.GetTokenAsync, grpcRequest, ExtensionMetadataWithLogEntry.Current.GetMetadata());

            return new TokenResponse
            {
                AccessToken = grpcResponse.AccessToken,
                RefreshToken = grpcResponse.RefreshToken,
                ExpiresIn = grpcResponse.ExpiresIn,
                Scope = grpcResponse.Scope,
                TokenType = grpcResponse.TokenType
            };
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
