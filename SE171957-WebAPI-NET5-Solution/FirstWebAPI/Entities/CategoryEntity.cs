﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FirstWebAPI.Entities
{
    [Table("Category")]
    public class CategoryEntity
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
