using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaseService.Models
{
    public abstract class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        public bool Deleted { get; set; } = false;
    }
}