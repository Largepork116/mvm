using DavidMorales.Domain.Security.Authentication;
using DavidMorales.Services.Api;
using DavidMorales.Services.Api.Authorization;
using DavidMorales.WebHost.Configurations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DavidMorales.WebHost
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IHttpContextAccessor HttpContextAccessor { get; set; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Api Rest Configuration
            RestServiceConfiguration.ConfigureServices(services);

            // Database configuration
            DatabaseConfiguration.ConfigureServices(services, Configuration);

            // Identity service configuration
            IdentityServiceConfiguration.ConfigureServices(services, Configuration);

            // Authorization
            AppAuthorizationConfiguration.ConfigureAuthorization(services);

            // Authentication configuration
            AuthenticationConfiguration.ConfigureServices(services, Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DavidMorales.WebHost", Version = "v1" });
            });



            // Identity
            services.AddTransient<AppIdentity>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Log
            services.AddLogging();

            // Cors
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DavidMorales.WebHost v1"));

                app.UseDeveloperExceptionPage();

                app.UseCors(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DavidMorales.WebHost v1"));
                app.UseDeveloperExceptionPage();

                app.UseCors(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            }

            RestServiceConfiguration.Configure(app);
            DatabaseConfiguration.Configure(app);
            IdentityServiceConfiguration.Configure(app);
            AuthenticationConfiguration.Configure(app);
            AppAuthorizationConfiguration.Configure(app);
        }
    }
}
