namespace ModelsServices.ViewModels
{
    public class CategoryProduit
    {
        public int Num { get; set; }
        public Guid Code { get; set; }
        public string? Désignation { get; set; }
        public int StockInitial { get; set; }
        public string? PrixAchat { get; set; }
    }
}
