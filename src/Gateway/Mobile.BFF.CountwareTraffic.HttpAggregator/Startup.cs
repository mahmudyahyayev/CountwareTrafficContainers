using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Countware.Traffic.Observability;
using CountwareTraffic.Services.Areas.Grpc;
using CountwareTraffic.Services.Devices.Grpc;
using CountwareTraffic.Services.Identity.Grpc;
using Grpc.Net.Client.Web;
using Mhd.Framework.Api;
using Mhd.Framework.Grpc.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        
        public void ConfigureServices(IServiceCollection services)
        {
            Mhd.Framework.Ioc.IoCGenerator.DoTNet.Current.Start(services, Configuration);
            services.AddControllers();
            services.AddCors(o =>
            {
                o.AddPolicy("Gateway", builder =>
                {
                    builder.SetIsOriginAllowed((host) => true);
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.AllowCredentials();
                });
            });
            services.AddSwagger();
            services.AddGrpcServices();
            services.ConfigureAuthService();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger()
               .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mobile.BFF.CountwareTraffic.HttpAggregator v1"))
               .UseHttpsRedirection()
               .UseRouting()
               .UseCors("Gateway")
               .UseAuthentication()
               .UseAuthorization()
               .UseGrpcWeb()
               .UserCorrelationId()
               .UseExceptionMiddleware()
               .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGrpcServices(this IServiceCollection services)
        {
           var urlsConfig =  services.BuildServiceProvider().GetRequiredService<IOptions<UrlsConfig>>().Value;

            #region grpcArea microservice

            services.AddGrpcClient<Subarea.SubareaClient>(options => { options.Address = new Uri(urlsConfig.GrpcArea); })
                .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler())
                {
                    HttpVersion = new Version(1, 1)
                })
                .RegisterClientInterceptors();


            services.AddGrpcClient<Company.CompanyClient>(options => { options.Address = new Uri(urlsConfig.GrpcArea); })
                .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler())
                {
                    HttpVersion = new Version(1, 1)
                })
                .RegisterClientInterceptors();


            services.AddGrpcClient<Area.AreaClient>(options => { options.Address = new Uri(urlsConfig.GrpcArea); })
                .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler())
                {
                    HttpVersion = new Version(1, 1)
                })
                .RegisterClientInterceptors();


            services.AddGrpcClient<District.DistrictClient>(options => { options.Address = new Uri(urlsConfig.GrpcArea); })
                .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler())
                {
                    HttpVersion = new Version(1, 1)
                })
                .RegisterClientInterceptors();


            services.AddGrpcClient<City.CityClient>(options => { options.Address = new Uri(urlsConfig.GrpcArea); })
                .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler())
                {
                    HttpVersion = new Version(1, 1)
                })
                .RegisterClientInterceptors();


            services.AddGrpcClient<Country.CountryClient>(options => { options.Address = new Uri(urlsConfig.GrpcArea); })
                .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler())
                {
                    HttpVersion = new Version(1, 1)
                })
                .RegisterClientInterceptors();

            #endregion grpcArea microservice


            #region grpcDevice microservice
            
            services.AddGrpcClient<Device.DeviceClient>(options => { options.Address = new Uri(urlsConfig.GrpcDevice); })
                .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler())
                {
                    HttpVersion = new Version(1, 1)
                })
                .RegisterClientInterceptors();

            #endregion grpcDevice microservice


            #region grpcUser microservice

            services.AddGrpcClient<User.UserClient>(options => { options.Address = new Uri(urlsConfig.GrpcUser); })
               .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler())
               {
                   HttpVersion = new Version(1, 1)
               })
               .RegisterClientInterceptors();

            #endregion grpcUser microservice

            return services;
        }

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
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfig.SignKey)),
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
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Mobile.BFF.CountwareTraffic.HttpAggregator", Version = "v1" });

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
