using Mhd.Framework.Core;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class ChangeSubAreaRequest : RequestValidate
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
