using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PostCodeAPI.Common.Impl;
using PostCodeAPI.Common.Interface;
using PostCodeAPI.Common.Model;
using PostCodeAPI.Middleware;
using PostCodeAPI.Service.AutoMapperProfiles;
using PostCodeAPI.Service.Impl;
using PostCodeAPI.Service.Interface;
using System;

namespace PostCodeAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
                    });
            });

            services.AddLogging(config =>
            {
                config.AddAWSProvider(Configuration.GetAWSLoggingConfigSection());
                config.SetMinimumLevel(LogLevel.Debug);
            });

            services.AddControllers();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            var config = new EnvironmentConfiguration(Configuration.GetSection("PostalCode").Get<EnvironmentConfigurationSection>());

            services.AddSingleton<IEnvironmentConfiguration>(_ => config);
            string baseURI = config.GetPostCodeBaseURI();
            services.AddSingleton<IPostCodeServices, PostCodeServices>();
            services.AddSingleton<IHttpClientRepository, HttpClientRepository>();
            services.AddHttpClient("PostCodesAPI", c => c.BaseAddress = new Uri(baseURI));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware(typeof(ExceptionMiddleware));

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });
        }
    }
}
