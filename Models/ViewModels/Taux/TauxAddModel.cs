using Utilities;
using ViewModels;

public class TauxAddModel : BaseModel
{
    public decimal ValueMonnaie1 { get; set; }
    public Monnaie Monnaie1 { get; set; }
    public decimal ValueMonnaie2 { get; set; }
    public Monnaie Monnaie2 { get; set; }
    public int IdPointVente { get; set; }
}