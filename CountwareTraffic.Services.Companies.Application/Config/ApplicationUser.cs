using Sensormatic.Tool.Ioc;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class ApplicationUser : IConfigurationOptions
    {
        public Guid Id { get; set; }
    }
}
