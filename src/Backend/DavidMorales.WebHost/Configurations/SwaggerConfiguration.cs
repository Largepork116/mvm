using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DavidMorales.WebHost.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "API para prueba técnica",
                    Description = "API para prueba técnica, construido en ASP.NET Core 5 Web API",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "demorales13@gmail.com",
                        Name = "David Morales",
                        Url = new System.Uri("https://www.linkedin.com/in/david-eduardo-morales-mart%C3%ADnez-98b96351/")
                    }
                });
            });

            return services;
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DavidMorales v1"));
        }
    }
}
