namespace Models.OldModels
{
    public class Category : TableBase
    {
        public string Designation { get; set; }
        public virtual ICollection<Produit> Produits { get; } = new List<Produit>();
    }
}
