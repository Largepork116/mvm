using DavidMorales.Domain.Authorization;

using Microsoft.AspNetCore.Authorization;

using System.Threading.Tasks;

namespace DavidMorales.Services.Api.Authorization
{
    public class AppAuthorizationHandler : AuthorizationHandler<AppPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AppPermissionRequirement requirement)
        {
            var user = context.User;

            if (user.IsInRole(AppRoles.SuperAdmin.Name))
                context.Succeed(requirement);

            if (user.HasClaim(c => c.Type == requirement.Type && c.Value == requirement.Value))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
