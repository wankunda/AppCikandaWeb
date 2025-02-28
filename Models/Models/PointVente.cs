using System.ComponentModel.DataAnnotations;

namespace Models;

public class PointVente : TableBase
{
    [MaxLength(20)]
    public string? Designation { get; set; }
    public virtual ICollection<PrixVente> PrixVentes { get; set; } = new List<PrixVente>();
    public virtual ICollection<Command> Commands { get; set; }= new List<Command>();
    public virtual ICollection<Taux> Tauxes { get; set; }= new List<Taux>();
    public virtual ICollection<Depense> Depenses { get; set; }= new List<Depense>();
}