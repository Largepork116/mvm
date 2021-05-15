using DavidMorales.Domain.Exceptions;

using System.Collections.Generic;
using System.Linq;

namespace DavidMorales.Domain.Enums
{
    public sealed class DocumentEnum
    {
        public static readonly DocumentEnum INTERNAL = new DocumentEnum("CI", nameof(INTERNAL));
        public static readonly DocumentEnum EXTERNAL = new DocumentEnum("CE", nameof(EXTERNAL));

        public string Id { get; private set; }
        public string Name { get; private set; }

        private DocumentEnum(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IReadOnlyCollection<DocumentEnum> Get()
        {
            return new[] { INTERNAL, EXTERNAL };
        }

        public static DocumentEnum FindBy(string id)
        {
            var state = Get().SingleOrDefault(s => s.Id == id);
            if (state == null)
            {
                var values = Get().Select(x => x.Id);
                throw new AppException($"Invalid value {nameof(DocumentEnum)} {id}. {string.Join(",", values)}");
            }

            return state;
        }
    }
}
