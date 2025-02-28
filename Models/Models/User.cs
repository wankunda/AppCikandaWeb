using System.ComponentModel.DataAnnotations;

namespace Models;

public class User : TableBase
{
    public int IdPointVente { get; set; }
    [MaxLength(50)]
    public string? Username { get; set; }
    [MaxLength(50)]
    public string? Email { get; set; }
    [MaxLength(20)]
    public string? Nom { get; set; }
    [MaxLength(15)]
    public string? Prenom { get; set; }
    [MaxLength(20)]
    public string? Postnom { get; set; }
    public string? Password { get; set; }
    public string? UserRole { get; set; }
    public string? Photo { get; set; }
}