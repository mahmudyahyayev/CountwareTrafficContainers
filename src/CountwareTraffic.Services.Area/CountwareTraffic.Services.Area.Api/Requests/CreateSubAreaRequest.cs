using Mhd.Framework.Core;

namespace CountwareTraffic.Services.Areas.Api
{
    public class CreateSubAreaRequest : RequestValidate
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(Name)));
        }
    }
}
