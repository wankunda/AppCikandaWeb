using Utilities;
using System.ComponentModel.DataAnnotations;

namespace ViewModels;

public class ArticleAddModel : BaseModel
{
    [Required(ErrorMessage ="La désignation de l'article est obligatoire")]
    public string? Designation { get; set; }
    public int StockInitial { get; set; }
    public int StockSecurite { get; set; }
    public float PrixAchat { get; set; }
    public string? Image { get; set; }
    public Monnaie Monnaie { get; set; }
    public int IdCategory { get; set; }
    public string? Category { get; set; }
}