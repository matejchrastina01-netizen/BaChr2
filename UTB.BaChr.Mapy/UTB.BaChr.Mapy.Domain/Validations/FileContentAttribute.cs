using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace UTB.BaChr.Mapy.Domain.Validations
{
    public class FileContentAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long _maxFileSize = 2 * 1024 * 1024; // 2 MB

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // Pokud je soubor null, nevadí (povinnost řeší atribut [Required])
                return ValidationResult.Success;
            }

            if (value is IFormFile file)
            {
                // 1. Kontrola velikosti
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"Soubor je příliš velký. Maximální velikost je {_maxFileSize / 1024 / 1024} MB.");
                }

                // 2. Kontrola přípony
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_allowedExtensions.Contains(extension))
                {
                    return new ValidationResult($"Tento typ souboru není povolen. Povolené jsou: {string.Join(", ", _allowedExtensions)}");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Neplatný typ souboru.");
        }
    }
}