using Microsoft.AspNetCore.Authorization;

namespace DavidMorales.Services.Api.Authorization
{
    public class AppPermissionRequirement : IAuthorizationRequirement
    {
        public AppPermissionRequirement(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; private set; }
        public string Value { get; private set; }
    }
}
