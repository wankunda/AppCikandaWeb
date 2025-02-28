namespace ViewModels
{
    public class BaseShowModel
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public string? Code { get; set; }
        public string? DateCreated { get; set; }
        public string? DateUpdated { get; set; }
        public string? LastSynchronized { get; set; }
        public bool Synchronized { get; set; }
    }
}
