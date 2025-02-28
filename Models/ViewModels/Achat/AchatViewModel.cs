using Models;

namespace ViewModels
{
    public class AchatViewModel : BaseShowModel
    {
        public string? Designation { get; set; }
        public virtual ICollection<ProduitAchatViewModel> Produits { get; set; } = new List<ProduitAchatViewModel>();
    }
}
