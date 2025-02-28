namespace ViewModels;

public class BaseModel
{
    public int Id { get; set; }
    public Guid Code { get; set; } = Guid.NewGuid();
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateUpdated { get; set; }
    public DateTime LastSynchronized { get; set; }
    public bool Delete { get; set; } = false;
    public bool Synchronized { get; set; } = false;
}