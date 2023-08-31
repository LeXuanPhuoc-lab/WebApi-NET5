using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Models
{
    public class AccountModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int StudentId { get; set; }
        public string Email { get; set; }
        public virtual StudentModel Student { get; set; }
    }
}
