using Utilities;

namespace Models.OldModels
{
    public class Produit : TableBase
    {
        public string Designation { get; set; }
        public string? Image { get; set; }
        public int StockInitial { get; set; }
        public int PrixAchat { get; set; }
        public Monnaie Monnaie { get; set; }
        public Guid IdCategory { get; set; }
        public Category? Category { get; set; }
        public virtual ICollection<ProduitsAppro> Appros { get; } = new List<ProduitsAppro>();
        public virtual ICollection<PrixVente> PrixVentes { get; } = new List<PrixVente>();
    }
}
