using Sensormatic.Tool.Core;

namespace CountwareTraffic.Services.Companies.Api
{
    public class CreateSubAreaRequest : SensormaticRequestValidate
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
