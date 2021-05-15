using AutoMapper;

using DavidMorales.Domain.Interfaces;
using DavidMorales.Domain.Interfaces.Services;
using DavidMorales.Infrastructure.Data;
using DavidMorales.Infrastructure.Files;
using DavidMorales.Services.Api.Automapper;
using DavidMorales.Services.Api.Helpers;
using DavidMorales.Services.AppServices;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace DavidMorales.Services.Api
{
    public static class RestServiceConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            // Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers(options =>
            {
                options.Filters.Add<HttpResponseExceptionFilter>();
                options.Filters.Add<ValidationFilter>();
            })
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<MappingProfile>());

            // Versionamiento del API
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
            });

            // Survey Services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IFileStoreService, FileStoreService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ILogDataChangeService, LogDataChangeService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
