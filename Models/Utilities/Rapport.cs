using Models.OldModels;

namespace Utilities
{
    public class Rapport
    {
        public List<Depense> Depenses { get; set; } = new();
        public List<Command> Commands { get; set; } = new();
        public List<ProduitsVendus> ProduitsVendus { get; set; } = new();
        public List<User> Users { get; set; } = new();
        public List<PrixVente> PrixVentes { get; set; } = new();
        public List<Produit> Produits { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<PointVente> PointVentes { get; set; } = new();
    }
}
