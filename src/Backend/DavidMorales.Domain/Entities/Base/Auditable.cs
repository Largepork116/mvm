using System;
using System.ComponentModel.DataAnnotations;

namespace DavidMorales.Domain.Entities.Base
{
    public class Auditable
    {
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt  { get; set; }
    }
}
