using System.ComponentModel.DataAnnotations;

namespace UTB.BaChr.Mapy.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Jméno je povinné")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email je povinný")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Heslo je povinné")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Hesla se neshodují")]
        public string ConfirmPassword { get; set; }
    }
}