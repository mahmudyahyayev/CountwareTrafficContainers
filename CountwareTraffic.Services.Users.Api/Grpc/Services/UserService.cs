using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using CountwareTraffic.Services.Users.Application;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Users.Grpc
{
    public class UserService : User.UserBase
    {
        private readonly ILogger<UserService> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public UserService(ILogger<UserService> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public async override Task<GetTokenResponse> GetToken(GetTokenRequest request, ServerCallContext context)
        {
            var response  = await _queryDispatcher.QueryAsync(new GetToken
            {
                ClientId = request.ClientId,
                Username = request.Username,
                ClientSecret = request.ClientSecret,
                GrantType = request.GrantType,
                Password = request.Password,
                RefreshToken = request.RefreshToken
            });

            return new GetTokenResponse
            {
                AccessToken = response.AccessToken,
                RefreshToken = response.RefreshToken,
                ExpiresIn = response.ExpiresIn,
                Scope = response.Scope,
                TokenType = response.TokenType
            };
        }
    }
}
