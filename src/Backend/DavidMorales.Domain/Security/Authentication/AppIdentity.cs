using Microsoft.AspNetCore.Http;

using System.Linq;


namespace DavidMorales.Domain.Security.Authentication
{
    public class AppIdentity
    {
        private readonly HttpContext _context;

        public AppIdentity(IHttpContextAccessor contextAccessor)
        {
            _context = contextAccessor.HttpContext;
        }

        public string Username
        {
            get
            {
                var userName = "SystemGenerated";
                if (_context != null)
                {
                    if (_context.User != null)
                    {
                        var identity = _context.User.Identity;

                        if (identity != null && identity.IsAuthenticated)
                        {
                            userName = _context.User?.Claims?.FirstOrDefault(x => x.Type == "Username")?.Value;
                        }
                    }
                }
                return userName;
            }
        }

        public int UserId
        {
            get
            {
                var userId = 0;
                if (_context != null)
                {
                    if (_context.User != null)
                    {
                        var identity = _context.User.Identity;

                        if (identity != null && identity.IsAuthenticated)
                        {
                            int.TryParse(_context.User?.Claims?.FirstOrDefault(x => x.Type == "UserId")?.Value, out userId);
                        }
                    }
                }
                return userId;
            }
        }

        public string Role
        {
            get
            {
                var role = "SystemGenerated";
                if (_context != null)
                {
                    if (_context.User != null)
                    {
                        var identity = _context.User.Identity;

                        if (identity != null && identity.IsAuthenticated)
                        {
                            role = _context.User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                        }
                    }
                }
                return role;
            }
        }

        public string IpAddress
        {
            get
            {
                return _context.Connection?.RemoteIpAddress?.ToString();
            }
        }
    }

}
