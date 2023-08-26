using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Entities
{
    [Table("User")]
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }
        public string FullName { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
