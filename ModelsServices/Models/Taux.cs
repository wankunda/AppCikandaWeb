namespace ModelsServices.Models
{
    public class Taux : BaseConfig
    {
        public Guid IdPointVente { get; set; }
        public DateTime DateJour { get; set; }
        public int Value { get; set; }
    }
}
