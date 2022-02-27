using CountwareTraffic.Services.Companies.Api;
using Grpc.Core;

namespace CountwareTraffic.Services.Companies.Grpc
{
    public static class ServerCallContextExtensions
    {
        public static void GetUser(this ServerCallContext context)
        {
            UserClaims userClaims = new UserClaims();
            var user = context.GetHttpContext().User;
        }
    }
}
