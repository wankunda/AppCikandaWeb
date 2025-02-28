using System.ComponentModel.DataAnnotations;

namespace Models;

public class ProduitVendu : TableBase
{
    public int IdCommand { get; set; }
    public Command? Command { get; set; }
    public int IdPrixVente { get; set; }
    public PrixVente? PrixVente { get; set; }
    public int Quantite { get; set; }
    [MaxLength(1)]
    public int Statut { get; set; }
    public string? Raison { get; set; }
}