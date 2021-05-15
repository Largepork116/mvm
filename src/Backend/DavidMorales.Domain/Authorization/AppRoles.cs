using DavidMorales.Domain.Exceptions;

using System.Collections.Generic;
using System.Linq;

namespace DavidMorales.Domain.Authorization
{
    public sealed class AppRoles
    {
        public static readonly AppRoles SuperAdmin = new AppRoles(nameof(SuperAdmin), AppTypes.WEB.Name, "SUPER ADMIN");
        public static readonly AppRoles DocumentManager = new AppRoles(nameof(DocumentManager), AppTypes.WEB.Name, "GESTOR DOCUMENTAL");
        public static readonly AppRoles User = new AppRoles(nameof(User), AppTypes.WEB.Name, "USUARIO");

        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Alias { get; private set; }

        private AppRoles(string name, string type, string alias)
        {
            Name = name;
            Type = type;
            Alias = alias;
        }

        public static IReadOnlyCollection<AppRoles> Get()
        {
            return new[] { SuperAdmin, DocumentManager, User };
        }


        public static AppRoles FindByName(string name)
        {
            var state = Get().SingleOrDefault(s => s.Name == name);
            if (state == null)
            {
                var values = Get().Select(x => x.Name);
                throw new AppException($"Invalid value {nameof(AppRoles)} {name}. {string.Join(",", values)}");
            }

            return state;
        }
    }
}
