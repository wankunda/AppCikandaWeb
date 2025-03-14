using Utilities;

namespace ViewModels
{
    public class TauxViewModel : BaseShowModel
    {
        public Money? MonnaieLocale { get; set; }
        public Money? MonnaieConvertie { get; set; }
        public string? PointVente { get; set; }
    }
}
