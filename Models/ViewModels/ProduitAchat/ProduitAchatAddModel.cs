using Utilities;

namespace ViewModels;

public class ProduitAchatAddModel : BaseModel
{
    public int IdArticle { get; set; }
    public int IdAchat { get; set; }    
    public float CoutAchat { get; set; }
    public Monnaie Monnaie { get; set; }
    public int Quantite { get; set; }
}