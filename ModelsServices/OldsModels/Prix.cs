using ModelsServices.Utilities;

namespace ModelsServices.OldsModels
{
    public class Prix
    {
        public Guid Id { get; set; }
        public float Cout { get; set; }
        public Monnaie Monnaie { get; set; }
        public Guid IdProduit { get; set; }
        public Guid IdPointVente { get; set; }
        public bool Active { get; set; }
    }
}
