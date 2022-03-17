using CountwareTraffic.Services.Areas.Api;
using Grpc.Core;

namespace CountwareTraffic.Services.Areas.Grpc
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
