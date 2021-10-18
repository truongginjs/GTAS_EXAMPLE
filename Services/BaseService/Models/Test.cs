using System.ComponentModel.DataAnnotations;

namespace BaseService.Models
{
    public class Test : BaseModel
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}