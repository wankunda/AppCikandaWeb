using Utilities;

namespace ViewModels
{
    public class DepenseViewModel : BaseShowModel
    {
        public string? PointVente { get; set; }
        public string? Beneficiaire { get; set; }
        public string? Motif { get; set; }
        public Money Montant { get; set; }
    }
}
