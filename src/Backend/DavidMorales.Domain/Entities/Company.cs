
using DavidMorales.Domain.Entities.Base;

using System.ComponentModel.DataAnnotations;

namespace DavidMorales.Domain.Entities
{
    public class Company : Auditable
    {
        [Key]
        public int CompanyId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
