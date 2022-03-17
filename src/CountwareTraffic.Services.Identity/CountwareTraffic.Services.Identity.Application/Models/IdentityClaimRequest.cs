namespace CountwareTraffic.Services.Identity.Application
{
    public class IdentityClaimRequest
    {
        public IdentityClaimRequest(string type, string value)
        {
            Type = type;
            Value = value;
        }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
