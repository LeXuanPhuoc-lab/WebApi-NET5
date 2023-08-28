using FPTManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public interface IAccountService
    {
        public bool Login(string username, string password);
        public bool SignUp(Account account);
        public Account GetByUserName(string username);
    }
}
