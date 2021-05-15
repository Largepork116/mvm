
using DavidMorales.Domain.Entities.Base;

using System.ComponentModel.DataAnnotations;

namespace DavidMorales.Domain.Entities
{
    public class Person: Auditable
    {
        [Key]
        public int PersonId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [StringLength(22)]
        [Required]
        public string Phone { get; set; }

        // Navigation properties
        public int CompanyId { get; set; }
        public Company Company { get;set;}

        public AppUser User { get; set; }
    }
}
