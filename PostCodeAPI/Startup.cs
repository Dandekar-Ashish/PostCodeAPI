using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PostCodeAPI.Common.Impl;
using PostCodeAPI.Common.Interface;
using PostCodeAPI.Common.Model;
using PostCodeAPI.Service.Impl;
using PostCodeAPI.Service.Interface;

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

            var config = new EnvironmentConfiguration(Configuration.GetSection("PostalCode").Get<EnvironmentConfigurationSection>());

            services.AddSingleton<IEnvironmentConfiguration>(_ => config);
            string str = config.GetPostCodeBaseURI();
            services.AddSingleton<IPostCodeServices, PostCodeServices>();
            services.AddSingleton<IHttpClientRepository, HttpClientRepository>();
            services.AddHttpClient("PostCodesAPI", c => c.BaseAddress = new Uri(config.GetPostCodeBaseURI()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("LogInformation Test 1");
            logger.LogDebug("LogDebug Test 1");
            logger.LogError("LogError Test 1");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

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
