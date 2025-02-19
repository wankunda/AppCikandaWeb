using ModelsServices.Utilities;

namespace ModelsServices.Models
{
    public class ProduitsAppro : BaseConfig
    {
        public Guid IdProduit { get; set; }
        public Produit? Produit { get; set; }
        public Guid IdAppro { get; set; }
        public Appro Appro { get; set; }
        public int PrixAppro { get; set; }
        public Monnaie Monnaie { get; set; }
        public int QteAppro { get; set; }
    }
}
