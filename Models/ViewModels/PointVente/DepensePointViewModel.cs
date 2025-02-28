using Utilities;

namespace ViewModels
{
    public class DepensePointViewModel : BaseShowModel
    {
        public string? Beneficiaire { get; set; }
        public string? Motif { get; set; }
        public Money? Montant { get; set; }
    }
}
