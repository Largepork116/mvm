using DavidMorales.Domain.Security.Settings;
using DavidMorales.Infrastructure.Context;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DavidMorales.WebHost.Configurations
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Database settings
            services.Configure<DataBaseSettings>(configuration.GetSection(nameof(DataBaseSettings)));
            var databaseSettings = services.BuildServiceProvider()
                .GetService<IOptions<DataBaseSettings>>()
                .Value;

            // Database Migrations
            //var assemblyName = typeof(SurveyContext).Namespace;
            //services.AddDbContextPool<SurveyContext>(options =>
            //    options.UseSqlServer(databaseSettings.ContextConnection, optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName))
            //    .EnableSensitiveDataLogging());

            // Database
            services.AddDbContextPool<AppContext>(options =>
                options.UseSqlServer(databaseSettings.ContextConnection)
                .EnableSensitiveDataLogging());

            return services;
        }

        public static void Configure(IApplicationBuilder app)
        {

        }
    }
}
