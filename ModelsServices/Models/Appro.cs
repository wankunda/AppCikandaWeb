using System.ComponentModel.DataAnnotations;

namespace ModelsServices.Models
{
    public class Appro
    {
        [Key, Required]
        public Guid Id { get; set; }
        public DateTime DateJour { get; set; }
        public string? Designation { get; set; }
        public virtual ICollection<ProduitsAppro> Produits { get; } = new List<ProduitsAppro>();
    }
}
