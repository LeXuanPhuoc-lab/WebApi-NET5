using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FPTManager.Entities
{
    public partial class Account
    {   
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(150)]
        public string Password { get; set; }
        public int StudentId { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public virtual Student Student { get; set; }
    }
}
