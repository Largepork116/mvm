using DavidMorales.Domain.Entities.Base;

using System.ComponentModel.DataAnnotations;

namespace DavidMorales.Domain.Entities
{
    public class Document : Auditable
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        [StringLength(2)]
        public string Type { get; set; }

        // Navigation properties
        public int? InternalFileId { get; set; }
        public InternalFile InternalFile { get; set; }

        public int? ExternalFileId { get; set; }
        public ExternalFile ExternalFile { get; set; }

        public int SenderId { get; set; }
        public Person Sender { get; set; }
        public int AddresseeId { get; set; }
        public Person Addressee { get; set; }
    }
}
