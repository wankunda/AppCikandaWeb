using ModelsServices.Utilities;

namespace ModelsServices.Models
{
    public class Depense : BaseConfig
    {
        public Guid IdPointVente { get; set; }
        public DateTime DateJour { get; set; }
        public string? Beneficiare { get; set; }
        public string? Motif { get; set; }
        public int Montant { get; set; }
        public Monnaie Monnaie { get; set; }
    }
}
