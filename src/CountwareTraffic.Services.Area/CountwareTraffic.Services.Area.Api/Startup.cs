using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Convey;
using Countware.Traffic.Observability;
using CountwareTraffic.Services.Areas.Application;
using CountwareTraffic.Services.Areas.Grpc;
using CountwareTraffic.Services.Areas.Infrastructure;
using Mhd.Framework.Api;
using Mhd.Framework.Grpc.Server;
using Mhd.Framework.Queue;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CountwareTraffic.Services.Areas.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        
        public void ConfigureServices(IServiceCollection services)
        {
            Mhd.Framework.Ioc.IoCGenerator.DoTNet.Current.Start(services, Configuration);
            services.AddDbContext<AreaDbContext>(options =>
                                                 options.UseSqlServer(Configuration.GetConnectionString("AreaDbConnection"), x => x.UseNetTopologySuite()));

            services.AddConvey()
                    .AddApplication()
                    .Build();

            services.AddGrpc(options => options.RegistrServerInterceptors());

            services.AddHostedService<SubAreaSubscriber>();
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

            services.AddSwagger();
            services.ConfigureAuthService();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger()
               .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CountwareTraffic.Services.Areas.Api v1"))
               .UseHttpsRedirection()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseExceptionMiddleware()
               .UseCors("AllowAll")
               .UseGrpcWeb()
               .UserCorrelationId()
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapGrpcService<AreaService>().RequireCors("AllowAll").EnableGrpcWeb();
                    endpoints.MapGrpcService<SubAreaService>().RequireCors("AllowAll").EnableGrpcWeb();
                    endpoints.MapGrpcService<CompanyService>().RequireCors("AllowAll").EnableGrpcWeb();
                    endpoints.MapGrpcService<DistrictService>().RequireCors("AllowAll").EnableGrpcWeb();
                    endpoints.MapGrpcService<CityService>().RequireCors("AllowAll").EnableGrpcWeb();
                    endpoints.MapGrpcService<CountryService>().RequireCors("AllowAll").EnableGrpcWeb();

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
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "CountwareTraffic.Services.Areas.Api", Version = "v1" });

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
