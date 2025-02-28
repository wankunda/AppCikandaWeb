using System.ComponentModel.DataAnnotations;

namespace Models;
 public class Depense : TableBase
 {
    public int IdPointVente { get; set; }
    public PointVente? PointVente { get; set; }
    [MaxLength(35)]
    public string? Beneficiaire { get; set; }
    public string? Motif { get; set; }
    public float Montant { get; set; }
   [MaxLength(1)]
    public int Monnaie { get; set; }
 }