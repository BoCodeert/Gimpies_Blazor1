using System.ComponentModel.DataAnnotations;

namespace Gimpies_Blazor1.Database.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vul een gebruikersnaam in.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vul een wachtwoord in.")]

        public string Password { get; set; }
    }
}
