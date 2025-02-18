 using System.ComponentModel.DataAnnotations;

namespace ModelsServices.Models
{
    public class Category
    {
        [Key, Required]
        public Guid Id { get; set; }
        public string Libellé { get; set; }
        public virtual ICollection<Produit> Produits { get; } = new List<Produit>();
    }
}
