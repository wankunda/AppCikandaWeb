using Utilities;

namespace ViewModels
{
    public class PrixPointVenteViewModel : BaseShowModel
    {
        public Money? PrixVente { get; set; }
        public string? Article { get; set; }
        public bool Active { get; set; }
    }
}
