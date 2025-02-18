using ModelsServices.Utilities;
using System.ComponentModel.DataAnnotations;

namespace ModelsServices.Models
{
    public class ProduitsVendus
    {
        [Key, Required]
        public Guid Id { get; set; }
        public Guid IdCommand { get; set; }
        public Command Command { get; set; }
        public Guid IdPrixVente { get; set; }
        public PrixVente? PrixVente { get; set; }
        public int QteVendue { get; set; }
        public EtatVente EtatVente { get; set; }
        public string? Motif { get; set; }
    }
}
