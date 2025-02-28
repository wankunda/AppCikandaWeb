using System.ComponentModel.DataAnnotations;

namespace Models;

public class Category : TableBase
{
    [MaxLength(20)]
    public string? Designation { get; set; }
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}