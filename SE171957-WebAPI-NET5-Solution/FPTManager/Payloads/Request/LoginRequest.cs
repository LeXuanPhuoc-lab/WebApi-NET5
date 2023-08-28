using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Payloads.Request
{
    public class LoginRequest
    {
        public string Username { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string Password { get; set; }    
    }
}
