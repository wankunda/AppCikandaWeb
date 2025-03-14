namespace Models;

public class Taux : TableBase
{
    public decimal MonnaieLocale { get; set; }
    public int Monnaie1 { get; set; }
    public decimal MonnaieConvertie { get; set; }
    public int Monnaie2 { get; set; }
    public int IdPointVente { get; set; }
    public PointVente? PointVente { get; set; }
}