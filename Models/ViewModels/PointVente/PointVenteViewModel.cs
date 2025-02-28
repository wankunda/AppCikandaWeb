namespace ViewModels
{
    public class PointVenteViewModel : BaseShowModel
    {
        public string? Designation { get; set; }
        public virtual ICollection<PrixPointVenteViewModel> PrixVentes { get; set; } = new List<PrixPointVenteViewModel>();
        public virtual ICollection<DepensePointViewModel> Depenses { get; set; } = new List<DepensePointViewModel>();
        public virtual ICollection<TauxPointVenteViewModel> Taux { get; set; } = new List<TauxPointVenteViewModel>();
    }
}
