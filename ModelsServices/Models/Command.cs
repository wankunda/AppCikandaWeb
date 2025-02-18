using ModelsServices.Utilities;
using System.ComponentModel.DataAnnotations;

namespace ModelsServices.Models
{
    public class Command
    {
        [Key, Required]
        public Guid Id { get; set; }
        public Guid IdPointVente { get; set; }
        public PointVente PointVente { get; set; }
        public DateTime DateJour { get; set; }
        public string NomServeur { get; set; }
        public string NumCmd { get; set; }
        public string NomClient { get; set; }
        public EtatCommand EtatCommand { get; set; }

        public virtual ICollection<ProduitsVendus> Produits { get; } = new List<ProduitsVendus>();
    }
}
