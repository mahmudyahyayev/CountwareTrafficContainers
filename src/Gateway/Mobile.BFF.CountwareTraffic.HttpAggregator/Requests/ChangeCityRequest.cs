using Mhd.Framework.Core;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class ChangeCityRequest :  RequestValidate
    {
        public string Name { get; set; }
        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(Name)));
        }
    }
}
