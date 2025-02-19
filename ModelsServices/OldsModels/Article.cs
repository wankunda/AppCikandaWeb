using ModelsServices.Utilities;

namespace ModelsServices.OldsModels
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Designation { get; set; }
        public int StockInitial { get; set; }
        public int PrixAchat { get; set; }
        public Monnaie Monnaie { get; set; }
        public Guid IdCategory { get; set; }
    }
}
