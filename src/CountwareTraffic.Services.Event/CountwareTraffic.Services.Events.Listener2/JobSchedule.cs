using System;

namespace CountwareTraffic.Services.Events.Listener2
{
    public class JobSchedule
    {
        public JobSchedule(Type jobType, string cronExpression, string jobName)
        {
            JobType = jobType;
            CronExpression = cronExpression;
            JobName = jobName;
        }

        public Type JobType { get; }
        public string CronExpression { get; }
        public string JobName { get; }
    }
}
