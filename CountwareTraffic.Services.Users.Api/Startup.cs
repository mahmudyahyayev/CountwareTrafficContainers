using CountwareTraffic.Services.Users.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Convey;
using Sensormatic.Tool.Grpc.Server;
using Microsoft.AspNetCore.Http;
using CountwareTraffic.Services.Users.Grpc;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Countware.Traffic.CrossCC.Observability;

namespace CountwareTraffic.Services.Users.Api
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

            services.AddConvey()
                   .AddApplication()
                   .Build();

            services.AddGrpc(options => options.RegistrServerInterceptors());

            services.AddControllers().AddJsonOptions(a =>
            {
                a.JsonSerializerOptions.WriteIndented = true;
                a.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                a.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                a.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CountwareTraffic.Services.Users.Api", Version = "v1" });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CountwareTraffic.Services.Users.Api v1"));
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.UseGrpcWeb();
            app.UserCorrelationId();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<UserService>().RequireCors("AllowAll").EnableGrpcWeb();
                
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Grpc user server is running");
                });
            });
        }
    }
}
