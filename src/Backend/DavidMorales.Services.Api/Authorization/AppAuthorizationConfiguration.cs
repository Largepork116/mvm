using DavidMorales.Domain.Authorization;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DavidMorales.Services.Api.Authorization
{
    public class AppAuthorizationConfiguration
    {
        public static IServiceCollection ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                                            .RequireAuthenticatedUser()
                                            .Build();

                DhyloPolicy(options, AppPermissions.Document.Query);
                DhyloPolicy(options, AppPermissions.Document.View);
                DhyloPolicy(options, AppPermissions.Document.Add);
                DhyloPolicy(options, AppPermissions.Document.Edit);

                DhyloPolicy(options, AppPermissions.Company.Query);
                DhyloPolicy(options, AppPermissions.Company.View);
                DhyloPolicy(options, AppPermissions.Company.Add);
                DhyloPolicy(options, AppPermissions.Company.Edit);

                DhyloPolicy(options, AppPermissions.Person.Query);
                DhyloPolicy(options, AppPermissions.Person.View);
                DhyloPolicy(options, AppPermissions.Person.Add);
                DhyloPolicy(options, AppPermissions.Person.Edit);

                DhyloPolicy(options, AppPermissions.User.Query);
                DhyloPolicy(options, AppPermissions.User.View);
                DhyloPolicy(options, AppPermissions.User.Add);
                DhyloPolicy(options, AppPermissions.User.Edit);

                DhyloPolicy(options, AppPermissions.LogDataChange.Query);
            });


            services.AddSingleton<IAuthorizationHandler, AppAuthorizationHandler>();

            return services;
        }

        private static void DhyloPolicy(AuthorizationOptions options, string name)
        {
            options.AddPolicy(name, policy => policy.Requirements.Add(new AppPermissionRequirement(AppClaimTypes.Permission, name)));
        }

        public static void Configure(IApplicationBuilder app)
        {
        }
    }
}

