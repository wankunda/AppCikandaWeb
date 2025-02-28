using Utilities;

namespace ViewModels;

public class ProduitVenduAddModel : BaseModel
{
    public int IdCommand { get; set; }
    public int IdPrixVente { get; set; }
    public int Quantite { get; set; }
    public StatutVente Statut { get; set; }
    public string? Raison { get; set; }
}