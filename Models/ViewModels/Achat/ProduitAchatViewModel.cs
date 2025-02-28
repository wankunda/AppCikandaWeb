using Utilities;

namespace ViewModels
{
    public class ProduitAchatViewModel : BaseShowModel
    {
        public string? Article { get; set; }
        public Money? CoutAchat { get; set; }
        public int Quantite { get; set; }
        public Money? ValeurTotale { get; set; }
    }
}
