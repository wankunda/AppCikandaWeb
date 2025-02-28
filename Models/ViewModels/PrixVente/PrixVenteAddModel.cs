namespace ViewModels;
using Utilities;

public class PrixVenteAddModel : BaseModel
{
    public float Value { get; set; }
    public Monnaie Monnaie { get; set; }
    public int IdArticle { get; set; }
    public int IdPointVente { get; set; }
    public bool Active { get; set; }
}