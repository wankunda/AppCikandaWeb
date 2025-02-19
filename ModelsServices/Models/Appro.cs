namespace ModelsServices.Models
{
    public class Appro : BaseConfig
    {

        public DateTime DateJour { get; set; }
        public string? Designation { get; set; }
        public virtual ICollection<ProduitsAppro> Produits { get; } = new List<ProduitsAppro>();
    }
}
