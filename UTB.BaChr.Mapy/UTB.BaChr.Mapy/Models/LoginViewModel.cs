using System.ComponentModel.DataAnnotations;

namespace UTB.BaChr.Mapy.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email je povinný")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Heslo je povinné")]
        public string Password { get; set; }
    }
}