using Convey;
using Countware.Traffic.Observability;
using CountwareTraffic.Services.Events.Application;
using CountwareTraffic.Services.Events.Infrastructure;
using Mhd.Framework.Api;
using Mhd.Framework.Queue;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CountwareTraffic.Services.Events.Api
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

            services.AddHostedService<DeviceSubscriber>();
            services.AddHostedService<EventSubscriber>();
            services.AddHostedService<AutoCreateDeviceEventsConsumerSubsriber>();
            services.AddHostedService<AutoDeleteDeviceEventsConsumerSubsriber>();
            services.AddHostedService<AutoScaler>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CountwareTraffic.Services.Events.Api", Version = "v1" });
            });

            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();


            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CountwareTraffic.Services.Events.Api v1"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UserCorrelationId();
            app.UseSensormaticExceptionMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
