using System.Collections.Generic;

namespace DavidMorales.Domain.Authorization
{
    public class AppTypes
    {
        public static readonly AppTypes WEB = new AppTypes(nameof(WEB));

        public string Name { get; private set; }

        private AppTypes(string name)
        {
            Name = name;
        }

        public static IReadOnlyCollection<AppTypes> Get()
        {
            return new[] { WEB };
        }
    }
}
