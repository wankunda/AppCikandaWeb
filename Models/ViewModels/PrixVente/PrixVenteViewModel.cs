using Utilities;

namespace ViewModels
{
    public class PrixVenteViewModel : BaseShowModel
    {
        public Money? Value { get; set; }
        public string? Article { get; set; }
        public string? PointVente { get; set; }
        public bool Active { get; set; }
    }
}
