using System.ComponentModel.DataAnnotations;
using Utilities;

namespace ViewModels;

public class UserAddModel : BaseModel
{
    public int IdPointVente { get; set; }
    [Required(ErrorMessage ="Entrer le nom d'utilisateur, c'est obligatoire !!")]
    public string? Username { get; set; }
    [Required(ErrorMessage ="L'adresse mail est obligatoire !!")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Indiquer le nom")]
    public string? Nom { get; set; }
    public string? Prenom { get; set; }
    public string? Postnom { get; set; }
    public string? Password { get; set; }
    public UserRole UserRole { get; set; }
    public string? Photo { get; set; }
}