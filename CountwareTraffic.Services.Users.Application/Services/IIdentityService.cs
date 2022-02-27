using Sensormatic.Tool.Ioc;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Users.Application
{
    public interface IIdentityService : ISingletonDependency
    {
        Task<IdentityTokenResponse> GetTokenAsync(IdentityTokenRequest identityTokenRequest);
        Task<Guid> CreateUserAsync(string accessToken, IdentityCreateUserRequest identityCreateUserRequest);
        Task DeleteUserAsync(string accessToken, IdentityDeleteUserRequest identityDeleteUserRequest);
        Task UpdateUserClaimsAsync(string accessToken, Guid userId, IdentityUpdateUserClaimsRequest identityUpdateUserClaimsRequest);
        Task UpdatePasswordAsync(string accessToken, IdentityUpdatePasswordRequest identityUpdatePasswordRequest);
        Task<IdentityResetPasswordResponse> InitiateResetPasswordAsync(string accessToken, string identifier, IdentityResetPasswordRequest identityResetPasswordRequest);
        Task ResetPasswordAsync(string accessToken, string identifier, IdentityResetPasswordChangeRequest identityResetPasswordChangeRequest);
        Task<string> GenerateUsernameAsync(string accessToken, string name, string surname);
        Task<IdentityCheckUserResponse> CheckUserAsync(string accessToken, IdentityCheckUserRequest checkRequest);
        Task PatchUserAsync(string accessToken, IdentityPatchUserRequest identityUpdatePersonalInformationRequest);
    }
}
