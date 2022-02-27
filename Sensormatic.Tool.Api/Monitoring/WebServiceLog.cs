namespace Sensormatic.Tool.Api
{
    public class WebServiceLog
    {
        public MonitorLog MonitorLog { get; set; }
        public ServiceLog ServiceLog { get; internal set; }
    }
}