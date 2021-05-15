using Microsoft.AspNetCore.Identity;

using System;

namespace DavidMorales.Domain.Entities
{
    public class AppUser : IdentityUser<Int64>
    {
        // Navigations properties
        public int PersonId { get; set; }
        public Person Person { get; set; }

    }
}
