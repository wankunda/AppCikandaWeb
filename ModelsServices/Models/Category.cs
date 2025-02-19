namespace ModelsServices.Models
{
    public class Category : BaseConfig
    {
        public string Libellé { get; set; }
        public virtual ICollection<Produit> Produits { get; } = new List<Produit>();
    }
}
