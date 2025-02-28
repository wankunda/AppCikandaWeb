using System.ComponentModel.DataAnnotations;

namespace Models;

public class Article : TableBase
{
    [MaxLength(25)]
    public string? Designation { get; set; }
    public int StockInitial { get; set; }
    public float PrixAchat { get; set; }
    [MaxLength(1)]
    public int Monnaie { get; set; }
    public int IdCategory { get; set; }
    public string? Image { get; set; }
    public Category? Category { get; set; }
    public virtual ICollection<ProduitAchat> ProduitAchats { get; set; } = new List<ProduitAchat>();
    public virtual ICollection<PrixVente> PrixVentes { get; set; } = new List<PrixVente>();
}