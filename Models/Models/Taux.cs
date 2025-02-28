namespace Models;

public class Taux : TableBase
{
    public decimal ValueMonnaie1 { get; set; }
    public int Monnaie1 { get; set; }
    public decimal ValueMonnaie2 { get; set; }
    public int Monnaie2 { get; set; }
    public int IdPointVente { get; set; }
    public PointVente? PointVente { get; set; }
}