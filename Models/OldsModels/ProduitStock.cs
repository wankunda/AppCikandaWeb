namespace Models.OldModels
{
    public class ProduitStock
    {
        public int Num { get; set; }
        public string Désignation { get; set; }
        public string? Catégorie { get; set; }
        public int StockInitial { get; set; }
        public int Appro { get; set; }
        public int Ventes { get; set; }
        public int EnStock { get; set; }
    }
}
