using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Models.Response
{
    public class AccountResponse
    {
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(150)]
        public string Password { get; set; }
        public int StudentId { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public virtual StudentResponse Student { get; set; }
    }
}
