using System.ComponentModel.DataAnnotations;

namespace ViewModels;

public class CategoryAddModel : BaseModel
{
    [Required(ErrorMessage = "La catégorie est obligatoire !!")]
    public string? Designation { get; set; }
}