using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseService.Models
{
    public class QC : BaseModel
    {
        [Required]
        public Guid ZoneId { get; set; }
        public QCZone Zone { get; set; }
        
        [NotMapped]
        public Factory Factory { get; set; }
        [NotMapped]
        public Buyer Buyer { get; set; }
        [NotMapped]
        public Product Product { get; set; }
        [Required]
        public DateTime InputDate { get; set; }
        [Required]
        public DateTime ExportDate { get; set; }
        [Required]
        public Guid TesterCheckId { get; set; }
        public Tester TesterCheck { get; set; }
        [Required]
        public DateTime DateCheck { get; set; }
        
        [Required]
        [Range(0, long.MaxValue)]
        public long Quantity { get; set; }
        public double DefectTotalPoint { get; set; }
        [NotMapped]
        public object Detail { get; set; }
        public string DetailJson { get; set; }
        [NotMapped]
        public DefectDetail DefectDetail { get; set; }
        public string DefectDetailJson { get; set; }
    }
}