using AutoMapper;
using FluentValidation;
using FPTManager.Entities;
using FPTManager.Models;
using FPTManager.Repositories;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public class AccountService : IAccountService
    {
        private readonly PRN211DemoADOContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AccountModel> _validator;

        public AccountService(PRN211DemoADOContext context, IValidator<AccountModel> validator,
            IAccountRepository accountRepository, IMapper mapper)
        {
            _context = context;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<bool>> CreateAsync(AccountModel account)
        {
            var validationResult = await _validator.ValidateAsync(account);
            if (!validationResult.IsValid)
            {
                var validationException = new ValidationException(validationResult.Errors);
                return new Result<bool>(validationException);
            }

            var accountEntity = _mapper.Map<Account>(account);
            return await _accountRepository.CreateAsync(accountEntity);
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
