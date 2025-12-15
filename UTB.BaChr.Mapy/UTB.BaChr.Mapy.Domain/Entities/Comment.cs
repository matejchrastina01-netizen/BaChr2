using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UTB.BaChr.Mapy.Domain.Entities
{
    [Table(nameof(Comment))]
    public class Comment
    {
        public int Id { get; set; }

        // PŘIDÁNO: Validace textu
        [Required(ErrorMessage = "Text komentáře je povinný.")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Komentář musí mít 3 až 500 znaků.")]
        public string Text { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public int LocationId { get; set; }
        [ForeignKey(nameof(LocationId))]
        public virtual Location? Location { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
    }
}