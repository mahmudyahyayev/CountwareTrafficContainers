using Convey;
using CountwareTraffic.Services.Events.Application;
using CountwareTraffic.Services.Events.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Quartz;
using Mhd.Framework.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Outbox.Scheduler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            Mhd.Framework.Ioc.IoCGenerator.DoTNet.Current.Start(services, Configuration);

            services.AddDbContext<EventDbContext>(options =>
                                                options.UseSqlServer(Configuration.GetConnectionString("EventDbConnection"), x => x.UseNetTopologySuite()));

            services.AddConvey()
                    .AddApplication()
                    .Build();

            var assembly = typeof(Mhd.Framework.Scheduler.Controllers.HomeController).Assembly;

            services.AddControllersWithViews()
               .AddApplicationPart(assembly)
               .AddRazorRuntimeCompilation();


            services.Configure<MvcRazorRuntimeCompilationOptions>(options => { options.FileProviders.Add(new EmbeddedFileProvider(assembly)); });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
            services.Configure<QuartzOptions>(configuration.GetSection("Quartz"));

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.AddJobListener<JobListener>();
                q.AddSchedulerListener<SchedulerListener>();
                q.AddTriggerListener<TriggerListener>();

                q.ScheduleJob<ProcessOutboxJob>(trigger => trigger
                    .WithIdentity("ProcessOutboxJob.trigger", "ProcessOutboxJob.trigger.group")
                    .StartNow()
                    .WithDescription("1 time in 20 seconds")
                    .WithSimpleSchedule(x => x
                       .RepeatForever()
                       .WithIntervalInSeconds(20)),
                    x => x
                       .WithIdentity("ProcessOutboxJob", "ProcessOutboxJob.group")
                       .WithDescription("ProcessOutbox of event microservice"));
            });

            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        }
    }
}
