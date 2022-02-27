using Convey;
using Convey.CQRS.Queries;
using CountwareTraffic.Services.Events.Application;
using CountwareTraffic.Services.Events.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Sensormatic.Tool.Queue;
using Sensormatic.Tool.Scheduler;
using System;
using System.Linq;

namespace CountwareTraffic.Services.Events.Listener.Scheduler
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Sensormatic.Tool.Ioc.IoCGenerator.DoTNet.Current.Start(services, Configuration);

            services.AddDbContext<EventDbContext>(options =>
                                                options.UseSqlServer(Configuration.GetConnectionString("EventDbConnection"), x => x.UseNetTopologySuite()));

            services.AddConvey()
                    .AddApplication()
                    .Build();

            var assembly = typeof(Sensormatic.Tool.Scheduler.Controllers.HomeController).Assembly;

            services.AddControllersWithViews()
               .AddApplicationPart(assembly)
               .AddRazorRuntimeCompilation();


            services.Configure<MvcRazorRuntimeCompilationOptions>(options => { options.FileProviders.Add(new EmbeddedFileProvider(assembly)); });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHostedService<AutoCreateDeviceEventJobSubscriber>();
            services.AddHostedService<AutoDeleteDeviceEventJobSubscriber>();
            services.AddHostedService<AutoScaler>();

            services.RegisterQuartzServices(Configuration);

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection()
               .UseStaticFiles(new StaticFileOptions { FileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory + "/wwwroot") })
               .UseRouting()
               .UseAuthorization()
               .UseEndpoints(endpoints => { endpoints.MapHub<JobHub>("/signalr"); })
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
    public static class QuartzRegistration
    {
        public static void RegisterQuartzServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<DeviceEventsListenerJob>();
            services.AddHostedService<QuartzHostedService>();
            services.Configure<QuartzOptions>(configuration.GetSection("Quartz"));

            var serviceProvider = services.BuildServiceProvider();
            var _queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();

            var devices = _queryDispatcher.QueryAsync(new GetDevices { }).Result;

            if (devices != null)
                devices.ToList().ForEach(device => services.AddSingleton(new JobSchedule(jobType: typeof(DeviceEventsListenerJob), cronExpression: "0/10 * * * * ?", jobName: device.Name)));
            
            services.AddQuartz(quartzConfigurator =>
            {
                quartzConfigurator.UseMicrosoftDependencyInjectionJobFactory();
                quartzConfigurator.AddJobListener<JobListener>();
                quartzConfigurator.AddSchedulerListener<SchedulerListener>();
                quartzConfigurator.AddTriggerListener<TriggerListener>();
            });

            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        }
    }

    //public static class QuartzRegistration
    //{
    //    public static void RegisterQuartzServices(this IServiceCollection services, IConfiguration configuration)
    //    {
    //        services.Configure<QuartzOptions>(configuration.GetSection("Quartz"));

    //        var serviceProvider = services.BuildServiceProvider();
    //        var _queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();

    //        var devices = _queryDispatcher.QueryAsync(new GetDevices { }).Result;

    //        if (devices != null)
    //        {
    //            services.AddQuartz(quartzConfigurator =>
    //            {
    //                quartzConfigurator.UseMicrosoftDependencyInjectionJobFactory();
    //                quartzConfigurator.AddJobListener<JobListener>();
    //                quartzConfigurator.AddSchedulerListener<SchedulerListener>();
    //                quartzConfigurator.AddTriggerListener<TriggerListener>();

    //            //    devices.ToList().ForEach(device =>
    //            //                                quartzConfigurator.ScheduleJob<DeviceEventsListenerJob>(trigger => trigger
    //            //                                    .WithIdentity(device.Name, device.Name)
    //            //                                    .StartNow()
    //            //                                    .WithDescription("1 time in 10 seconds")
    //            //                                    .WithSimpleSchedule(x => x
    //            //                                       .RepeatForever()
    //            //                                       .WithIntervalInSeconds(10)),
    //            //                                    jobConfiguration => jobConfiguration
    //            //                                       .WithIdentity(device.Name, device.Name)
    //            //                                       .WithDescription("Pull the events from device and insert db"))
    //            //);
    //            });

    //            services.AddQuartzServer(options =>
    //            {
    //                options.WaitForJobsToComplete = true;
    //            });
    //        }
    //    }
    //}
}
