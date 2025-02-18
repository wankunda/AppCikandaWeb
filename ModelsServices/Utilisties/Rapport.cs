namespace ModelsServices.Utilisties
{
    public class Rapport
    {
        public string Code { get; set; } = Configuration.Id + "-" + Configuration.Name;
        public List<Sortie> Depenses { get; set; } = new();
        public List<Commande> Commands { get; set; } = new();
        public List<ProduitVente> ProduitsVendus { get; set; } = new();
        public List<Utilisateur> Users { get; set; } = new();
        public List<Prix> PrixVentes { get; set; } = new();
        public List<Article> Produits { get; set; } = new();
        public List<Groupe> Categories { get; set; } = new();
        public List<LieuVente> PointVentes { get; set; } = new();
    }
}
