using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Entities
{
    [Table("Product")]
    public class ProductEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
  
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        
        public Byte Voucher { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryEntity category;
    }
}
