namespace Sensormatic.Tool.Scheduler
{
    public class JobExecutedResponse
    {
        public string JobName { get; set; }
        public string LastRunTime { get; set; }
        public string TriggerState { get; set; }
    }
}
