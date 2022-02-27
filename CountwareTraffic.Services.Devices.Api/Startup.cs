using Convey;
using CountwareTraffic.Services.Devices.Application;
using CountwareTraffic.Services.Devices.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sensormatic.Tool.Queue;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Sensormatic.Tool.Grpc.Server;
using Microsoft.AspNetCore.Http;
using Sensormatic.Tool.Api;
using CountwareTraffic.Services.Devices.Grpc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Reflection;
using System.IO;
using Countware.Traffic.CrossCC.Observability;

namespace CountwareTraffic.Services.Devices.Api
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
            Sensormatic.Tool.Ioc.IoCGenerator.DoTNet.Current.Start(services, Configuration);

            services.AddDbContext<DeviceDbContext>(options =>
                                                 options.UseSqlServer(Configuration.GetConnectionString("DeviceDbConnection"), x => x.UseNetTopologySuite()));
            services.AddConvey()
                    .AddApplication()
                    .Build();
            services.AddGrpc(options => options.RegistrServerInterceptors());

            services.AddHostedService<SubAreaSubscriber>();
            services.AddHostedService<DeviceSubscriber>();
            services.AddHostedService<AutoScaler>();

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

            services.AddControllers().AddJsonOptions(a =>
            {
                a.JsonSerializerOptions.WriteIndented = true;
                a.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                a.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                a.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            });

            services.AddControllers();

            services.AddSwagger();
            services.ConfigureAuthService();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger()
               .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CountwareTraffic.Services.Devices.Api v1"))
               .UseHttpsRedirection()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseSensormaticExceptionMiddleware()
               .UseCors("AllowAll")
               .UseGrpcWeb()
               .UserCorrelationId()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllers();
                   endpoints.MapGrpcService<DeviceService>().RequireCors("AllowAll").EnableGrpcWeb();

                   endpoints.MapGet("/", async context =>
                   {
                       await context.Response.WriteAsync("Grpc area server is running");
                   });
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

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "CountwareTraffic.Services.Devices.Api", Version = "v1" });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                swagger.IncludeXmlComments(xmlPath, true);

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
        }
    }
}
