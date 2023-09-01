using FPTManager.Entities;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PRN211DemoADOContext _context;

        public AccountRepository(PRN211DemoADOContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> CreateAsync(Account account)
        {
            _context.Accounts.Add(account);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
