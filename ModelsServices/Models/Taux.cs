using System.ComponentModel.DataAnnotations;

namespace ModelsServices.Models
{
    public class Taux
    {
        [Key, Required]
        public Guid Id { get; set; }
        public Guid IdPointVente { get; set; }
        public DateTime DateJour { get; set; }
        public int Value { get; set; }
    }
}
