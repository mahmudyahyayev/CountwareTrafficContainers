namespace Mhd.Framework.Scheduler
{
    public class JobExecutingResponse
    {
        public string JobName { get; set; }
        public string NextFireTime { get; set; }
        public string PreviousFireTime { get; set; }
        public string FireTime { get; set; }
    }
}
