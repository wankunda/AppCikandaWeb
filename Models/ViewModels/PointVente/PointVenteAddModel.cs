using System.ComponentModel.DataAnnotations;

namespace ViewModels;

public class PointVenteAddModel : BaseModel
{
    [Required(ErrorMessage ="Indiquer le nom du lieu de vente ou Point de vente")]
    public string? Designation { get; set; }
}