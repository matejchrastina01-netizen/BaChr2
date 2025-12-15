using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UTB.BaChr.Mapy.Domain.Entities
{
    [Table(nameof(Photo))]
    public class Photo
    {
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; } = string.Empty; // Cesta k souboru

        public int LocationId { get; set; }
        [ForeignKey(nameof(LocationId))]
        public virtual Location? Location { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
    }
}