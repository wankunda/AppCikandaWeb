using Utilities;

namespace ViewModels
{
    public class ArticleViewModel : BaseShowModel
    {
        public string? Designation { get; set; }
        public string? Image { get; set; }
        public int StockInitial { get; set; }
        public Money? PrixAchat { get; set; }
        public string? Category { get; set; }
        public virtual ICollection<PrixVenteArticleViewModel> PrixVentes { get; set; } = new List<PrixVenteArticleViewModel>();
    }
}
