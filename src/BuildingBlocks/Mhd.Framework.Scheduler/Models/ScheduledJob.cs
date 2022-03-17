namespace Mhd.Framework.Scheduler
{
    public class ScheduledJob
    {
        public string JobName { get; set; }
        public string Description { get; set; }
        public string TriggerDescription { get; set; }
        public string NextFireTime { get; set; }
        public string PreviousFireTime { get; set; }
        public string FireTime { get; set; }
        public string TriggerState { get; set; }
    }
}
