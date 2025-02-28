using System.ComponentModel.DataAnnotations;

namespace Models;

public class Achat : TableBase
{
    [MaxLength(20)]
    public string? Designation { get; set; }
    public virtual ICollection<ProduitAchat> Produits { get; set; } = new List<ProduitAchat>();
}