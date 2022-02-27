using Sensormatic.Tool.Core;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class ChangeCityRequest :  SensormaticRequestValidate
    {
        public string Name { get; set; }
        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(Name)));
        }
    }
}
