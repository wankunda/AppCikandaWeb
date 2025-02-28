using System.ComponentModel.DataAnnotations;

namespace Models;

 public class TableBase
 {
    [Key, Required]
    public int Id{get; set; }
    public string? Code {get; set;}   
    [MaxLength(10)]
    public string? DateCreated { get; set; }
    [MaxLength(10)]
    public string? DateUpdated { get; set; }
    [MaxLength(10)]
    public string? LastSynchronized { get; set; }
    public bool Delete { get; set; }
    public bool Synchronized { get; set; }
 }