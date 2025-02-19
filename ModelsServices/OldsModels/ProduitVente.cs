using ModelsServices.Utilities;

namespace ModelsServices.OldsModels
{
    public class ProduitVente
    {
        public Guid Id { get; set; }
        public Guid IdCommand { get; set; }
        public Guid IdPointVente { get; set; }
        public Guid IdPrixVente { get; set; }
        public int QteVendue { get; set; }
        public EtatVente EtatVente { get; set; }
        public string? Motif { get; set; }
    }
}
