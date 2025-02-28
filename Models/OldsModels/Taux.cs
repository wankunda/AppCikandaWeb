namespace Models.OldModels
{
    public class Taux : TableBase
    {
        public Guid IdPointVente { get; set; }
        public DateTime DateJour { get; set; }
        public int Value { get; set; }
    }
}
