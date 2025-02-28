using Models;
using Utilities;

namespace ViewModels
{
    public class CommandViewModel : BaseShowModel
    {
        public string? PointVente { get; set; }
        public string? NomServante { get; set; }
        public string? NumCmd { get; set; }
        public string? NomClient { get; set; }
        public string? Statut { get; set; }
        public virtual ICollection<ProduitCommandViewModel> ProduitVendus { get; set; } = new List<ProduitCommandViewModel>();
    }
}
