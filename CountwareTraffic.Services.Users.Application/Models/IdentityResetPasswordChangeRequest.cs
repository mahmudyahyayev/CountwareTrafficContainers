namespace CountwareTraffic.Services.Users.Application
{
    public class IdentityResetPasswordChangeRequest
    {
        public IdentityCommunicationType MessageType { get; set; }
        public IdentityUserIdentifierType IdentifierType { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}
