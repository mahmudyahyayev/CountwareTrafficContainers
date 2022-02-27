using Sensormatic.Tool.Ioc;
using System;

namespace CountwareTraffic.WorkerServices.SignalrHub
{
    public class ApplicationUser : IConfigurationOptions
    {
        public Guid Id { get; set; }
    }
}
