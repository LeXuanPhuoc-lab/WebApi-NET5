using FPTManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public class AccountService : IAccountService
    {
        private readonly PRN211DemoADOContext _context;

        public AccountService(PRN211DemoADOContext context)
        {
            _context = context;
        }

        public Account GetByUserName(string username)
        {
            return _context.Accounts // from
                           .Where(x => x.Username == username)//where
                           .FirstOrDefault();//select
        }

        public bool Login(string username, string password)
        {
            return _context.Accounts // find in all accounts
                           .Where(x => x.Username == username && x.Password == password) // where username, password
                           .FirstOrDefault() != null ? true : false; // exist return true, otherwise false
        }

        public bool SignUp(Account account)
        {
            _context.Accounts.Add(account);
            return (_context.SaveChanges() > 0) ? true : false;
        }
    }
}
