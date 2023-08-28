using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Payloads.Request
{
    public class SignUpRequest
    {
     
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string MidName { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
