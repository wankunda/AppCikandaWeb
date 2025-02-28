using Utilities;
using System.ComponentModel.DataAnnotations;

namespace ViewModels;

public class CommandAddModel : BaseModel
{
    public int IdPointVente { get; set; }
    [Required(ErrorMessage ="Indiquez le nom de la servante pour continuer")]
    public string? NomServante { get; set; }
    public string? NumCmd { get; set; }
    public string? NomClient { get; set; }
    public StatutCommand Statut { get; set; }
}