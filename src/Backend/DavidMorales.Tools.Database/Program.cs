using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Interfaces;
using DavidMorales.Domain.Security.Authentication;
using DavidMorales.Infrastructure.Data;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace DavidMorales.Tools.Database
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static IServiceProvider serviceProvider;

        private static void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["DatabaseSettings:ContextConnection"];

            // Entityframework
            // Entityframework
            services.AddDbContextPool<Infrastructure.Context.AppContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(connection);
            });

            // Se registra el Identity services.
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<Infrastructure.Context.AppContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                options.Lockout.MaxFailedAccessAttempts = 10;
            });

            services.AddLogging();



            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddTransient<AppIdentity>();

            services.AddTransient<SeedDataBase>();
        }

        public static void Main(string[] args)
        {
            try
            {
                // Se obtienen los datos de configuración
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Se crean los servicios que se usaran
                Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}Configurando servicios...");
                var services = new ServiceCollection();
                ConfigureServices(services);
                serviceProvider = services.BuildServiceProvider();

                // Se corre el proceso de creación de base de datos
                Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}Creando tablas e insertando datos...");
                var seeder = serviceProvider.GetRequiredService<SeedDataBase>();
                seeder.EnsureSeedData().Wait();

                Console.WriteLine($"{Environment.NewLine}Creación finalizada exitosamente...");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Se presentó un error en la creación de la bd");
                Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}{ex.Message}");
            }
            Console.ReadKey();
        }
    }
}
