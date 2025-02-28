using System.ComponentModel.DataAnnotations;

namespace Models;

public class PrixVente : TableBase
{
    public float Value { get; set; }
    [MaxLength(1)]
    public int Monnaie { get; set; }
    public int IdArticle { get; set; }
    public Article? Article { get; set; }
    public int IdPointVente { get; set; }
    public PointVente? PointVente { get; set; }
    public bool Active { get; set; }
    public virtual ICollection<ProduitVendu> ProduitVendus { get; set; } = new List<ProduitVendu>();
}