using AutoMapper;
using FPTManager.Entities;
using FPTManager.Models;
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
        private readonly IMapper _mapper;

        public AccountRepository(PRN211DemoADOContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Login(string username, string password)
        {
            return  _context.Accounts // find in all accounts
                           .Where(x => x.Username == username && x.Password == password) // where username, password
                           .FirstOrDefault() != null ? true : false; // exist return true, otherwise false
        }

        public async Task<bool> CreateAsync(Account account)
        {
            _context.Accounts.Add(account);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<AccountModel> GetByEmailAsync(string email)
        {
            var accountEntity = _context.Accounts // from
                                        .Where(x => x.Email.Equals(email))//where
                                        .FirstOrDefault();//select
            return _mapper.Map<AccountModel>(accountEntity);
        }

        public async Task<AccountModel> GetByUsernameAsync(string username)
        {
            var accountEntity = _context.Accounts // from
                                       .Where(x => x.Username.Equals(username))//where
                                       .FirstOrDefault();//select
            return _mapper.Map<AccountModel>(accountEntity);
        }

        public async Task<AccountModel> GetUsernameAsync(string email)
        {
            var accountEntity = _context.Accounts // from
                       .Where(x => x.Email.Equals(email))//where
                       .FirstOrDefault();//select
            return _mapper.Map<AccountModel>(accountEntity);
        }
    }
}
