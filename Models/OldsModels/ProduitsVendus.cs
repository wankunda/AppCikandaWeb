using Utilities;

namespace Models.OldModels
{
    public class ProduitsVendus : TableBase
    {
        public Guid IdCommand { get; set; }
        public Command Command { get; set; }
        public Guid IdPrixVente { get; set; }
        public PrixVente? PrixVente { get; set; }
        public int QteVendue { get; set; }
        public StatutVente EtatVente { get; set; }
        public string? Motif { get; set; }
    }
}
