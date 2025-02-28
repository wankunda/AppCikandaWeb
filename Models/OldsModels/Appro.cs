namespace Models.OldModels
{
    public class Appro : TableBase
    {

        public DateTime DateJour { get; set; }
        public string? Designation { get; set; }
        public virtual ICollection<ProduitsAppro> Produits { get; } = new List<ProduitsAppro>();
    }
}
