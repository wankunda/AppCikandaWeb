using System.ComponentModel.DataAnnotations;

namespace Models;

public class ProduitAchat : TableBase
{
    public int IdArticle { get; set; }
    public Article? Article { get; set; }
    public int IdAchat { get; set; }    
    public Achat? Achat { get; set; }  
    public float CoutAchat { get; set; }
    [MaxLength(1)]
    public int Monnaie { get; set; }
    public int Quantite { get; set; }
}