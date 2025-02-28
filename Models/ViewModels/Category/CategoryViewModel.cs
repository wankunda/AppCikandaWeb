namespace ViewModels
{
    public class CategoryViewModel : BaseShowModel
    {
        public string? Designation { get; set; }
        public virtual ICollection<CategoryProduitViewModel> Articles { get; set; } = new List<CategoryProduitViewModel>();
    }
}
