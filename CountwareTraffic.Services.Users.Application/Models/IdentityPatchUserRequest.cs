namespace CountwareTraffic.Services.Users.Application
{
    public class IdentityPatchUserRequest
    {
        public PatchRequestObject<string> Email { get; set; }
        public PatchRequestObject<string> CitizenshipId { get; set; }
        public PatchRequestObject<string> PhoneNumber { get; set; }
        public PatchRequestObject<bool> PhoneNumberConfirmed { get; set; }
        public PatchRequestObject<bool> EmailConfirmed { get; set; }
    }
    public class PatchRequestObject<T>
    {
        public T Value { get; set; }
    }
}
