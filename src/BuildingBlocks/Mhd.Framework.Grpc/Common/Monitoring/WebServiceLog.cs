namespace Mhd.Framework.Grpc.Common
{
    public class WebServiceLog 
    {
        public MonitorLog MonitorLog { get; set; }
        public ServiceLog ServiceLog { get; set; }
        public string CorrelationId { get; set; }
    }
}
