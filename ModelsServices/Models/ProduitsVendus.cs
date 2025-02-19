using ModelsServices.Utilities;

namespace ModelsServices.Models
{
    public class ProduitsVendus : BaseConfig
    {
        public Guid IdCommand { get; set; }
        public Command Command { get; set; }
        public Guid IdPrixVente { get; set; }
        public PrixVente? PrixVente { get; set; }
        public int QteVendue { get; set; }
        public EtatVente EtatVente { get; set; }
        public string? Motif { get; set; }
    }
}
