using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DavidMorales.Domain.Entities
{
    [Table("DataChanges", Schema = "log")]
    public class LogDataChange
    {
        [Key]
        public int DataChangeId { get; set; }

        [StringLength(50)]
        [Required]
        public string Table { get; set; }

        [Required]
        public int Pk { get; set; }

        [Required]
        public string Changes { get; set; }

        [Required]
        public string UpdatedBy { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
