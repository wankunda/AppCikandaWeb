using Utilities;
using System.ComponentModel.DataAnnotations;

namespace ViewModels;
public class DepenseAddModel : BaseModel
{
    public int IdPointVente { get; set; }
    [Required(ErrorMessage = "Indiquer le nom complet du bénéficiaire")]
    public string? Beneficiaire { get; set; }
    public string? Motif { get; set; }
    public string? Montant { get; set; }
}