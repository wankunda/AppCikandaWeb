using Utilities;

namespace Models.OldModels
{
    public class Command : TableBase
    {
        public Guid IdPointVente { get; set; }
        public PointVente PointVente { get; set; }
        public DateTime DateJour { get; set; }
        public string NomServeur { get; set; }
        public string NumCmd { get; set; }
        public string NomClient { get; set; }
        public StatutCommand EtatCommand { get; set; }

        public virtual ICollection<ProduitsVendus> Produits { get; } = new List<ProduitsVendus>();
    }
}
