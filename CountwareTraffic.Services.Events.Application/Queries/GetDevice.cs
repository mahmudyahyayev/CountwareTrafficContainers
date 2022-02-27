namespace CountwareTraffic.Services.Events.Application
{
    public class GetDevice :Convey.CQRS.Queries.IQuery<DeviceDto>
    {
        public string DeviceName { get; set; }
    }
}
