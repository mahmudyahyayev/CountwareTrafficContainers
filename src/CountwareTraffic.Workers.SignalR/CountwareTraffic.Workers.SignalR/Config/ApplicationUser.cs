using Mhd.Framework.Ioc;
using System;

namespace CountwareTraffic.Workers.SignalR
{
    public class ApplicationUser : IConfigurationOptions
    {
        public Guid Id { get; set; }
    }
}
