using Utilities;

namespace ViewModels
{
    public class ProduitCommandViewModel : BaseShowModel
    {
        public string? Article { get; set; }
        public Money? PrixVente { get; set; }
        public int Quantite { get; set; }
        public Money? ValeurTotale { get; set; }
        public string? Statut { get; set; }
        public string? Raison { get; set; }
    }
}
