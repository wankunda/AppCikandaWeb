using ModelsServices.Utilities;

namespace ModelsServices.OldsModels
{
    public class Utilisateur
    {
        public Guid Id { get; set; }
        public Guid IdPointVente { get; set; }
        public string? Username { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; }
    }
}
