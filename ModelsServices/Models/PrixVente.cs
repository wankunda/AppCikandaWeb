using ModelsServices.Utilities;
using System.ComponentModel.DataAnnotations;

namespace ModelsServices.Models
{
    public class PrixVente
    {
        [Key, Required]
        public Guid Id { get; set; }
        public float Cout { get; set; }
        public Monnaie Monnaie { get; set; }
        public Guid IdProduit { get; set; }
        public Produit Produit { get; set; }
        public Guid IdPointVente { get; set; }
        public PointVente PointVente { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<ProduitsVendus> Produits { get; } = new List<ProduitsVendus>();
    }
}
