using ModelsServices.Utilities;

namespace ModelsServices.OldsModels
{
    public class Commande
    {
        public Guid Id { get; set; }
        public Guid IdPointVente { get; set; }
        public DateTime DateJour { get; set; }
        public string NomServeur { get; set; }
        public string NumCmd { get; set; }
        public string NomClient { get; set; }
        public EtatCommand EtatCommand { get; set; }
    }
}
