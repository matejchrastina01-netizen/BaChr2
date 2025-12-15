using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using UTB.BaChr.Mapy.Domain.Validations;

namespace UTB.BaChr.Mapy.Models
{
    public class PhotoUploadViewModel
    {
        [Required]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Vyberte prosím soubor.")]
        [FileContent(ErrorMessage = "Soubor musí být obrázek (.jpg, .png) a menší než 2MB.")] // Náš vlastní atribut
        public IFormFile? File { get; set; }
    }
}