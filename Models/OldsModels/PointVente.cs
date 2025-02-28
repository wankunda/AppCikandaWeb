namespace Models.OldModels
{
    public class PointVente : TableBase
    {
        public string Designation { get; set; }
        public virtual ICollection<PrixVente> PrixVentes { get; } = new List<PrixVente>();
        public virtual ICollection<Command> Commands { get; } = new List<Command>();
    }
}
