using Utilities;

namespace ViewModels
{
    public class CategoryProduitViewModel: BaseShowModel
    {
        public string? Designation { get; set; }
        public int StockInitial { get; set; }
        public int StockSeuil { get; set; }
        public Money? PrixAchat { get; set; }
    }
}
