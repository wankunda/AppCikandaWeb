using System.ComponentModel.DataAnnotations;

namespace ModelsServices.Models
{
    public class PointVente
    {
        [Key, Required]
        public Guid Id { get; set; }
        public string Designation { get; set; }
        public virtual ICollection<PrixVente> PrixVentes { get; } = new List<PrixVente>();
        public virtual ICollection<Command> Commands { get; } = new List<Command>();
    }
}
