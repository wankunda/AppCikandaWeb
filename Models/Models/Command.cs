using System.ComponentModel.DataAnnotations;

namespace Models;

public class Command : TableBase
{
    public int IdPointVente { get; set; }
    public PointVente? PointVente { get; set; }
    [MaxLength(35)]
    public string? NomServante { get; set; }
    [MaxLength(10)]
    public string? NumCmd { get; set; }
    [MaxLength(35)]
    public string? NomClient { get; set; }
    [MaxLength(1)]
    public int Statut { get; set; }
    public virtual ICollection<ProduitVendu> ProduitVendus { get; set; } = new List<ProduitVendu>();
}