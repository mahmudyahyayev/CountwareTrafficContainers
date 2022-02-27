using Convey;
using CountwareTraffic.WorkerServices.SignalrHub.Application;
using CountwareTraffic.WorkerServices.SignalrHub.Consumer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sensormatic.Tool.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.SignalrHub
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            Sensormatic.Tool.Ioc.IoCGenerator.DoTNet.Current.Start(services, Configuration);

            services.AddConvey()
                   .AddApplication()
                   .Build();

            services.AddControllers();

            services.AddHostedService<SignalRSubscriber>();
            services.AddHostedService<AutoScaler>();

            services.AddCors(options =>
             {
                 options.AddPolicy("CorsPolicy",
                     builder => builder
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .SetIsOriginAllowed((host) => true)
                     .AllowCredentials());
             });

            services.AddSignalR();
            services.ConfigureAuthService();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseCors("CorsPolicy")
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<NotificationsHub>("/hub/notificationhub");
                });
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAuthService(this IServiceCollection services)
        {
            var authenticationConfig = services.BuildServiceProvider().GetRequiredService<IOptions<AuthenticationConfig>>().Value;

            services
                  .AddAuthentication(options =>
                  {
                      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                  })
                   .AddJwtBearer(cfg =>
                   {
                       cfg.RequireHttpsMetadata = true;
                       cfg.SaveToken = true;
                       cfg.TokenValidationParameters = new TokenValidationParameters()
                       {
                           IssuerSigningKey = new SymmetricSecurityKey(
                               Encoding.UTF8.GetBytes(authenticationConfig.SignKey)),
                           ValidateAudience = false,
                           ValidateIssuer = false,
                           ValidateLifetime = false,
                           RequireExpirationTime = false,
                           ClockSkew = TimeSpan.Zero,
                           ValidateIssuerSigningKey = true
                       };
                   });

            services.AddAuthorization();
        }
    }
}
