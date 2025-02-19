using System.ComponentModel.DataAnnotations;

namespace ModelsServices.Models
{
    public class BaseConfig
    {
        [Key, Required]
        public Guid Id { get; set; }
        public bool UpAsync { get; set; }
        public bool DownAsync { get; set; } = true;
        public DateTime DateCreated { get; set; } = DateTime.Now.Date;
        public DateTime DateAnsyc { get; set; } = DateTime.Now;
    }
}
