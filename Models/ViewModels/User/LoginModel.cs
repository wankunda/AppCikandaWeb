using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Entrer le nom d'utilisateur ou l'adresse mail, obligatoire !!")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Entrer le mot de password, obligatoire !!")]
        public string? Password { get; set; }
    }
}
