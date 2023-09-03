using AutoMapper;
using FluentValidation;
using FPTManager.Entities;
using FPTManager.Models;
using FPTManager.Repositories;
using LanguageExt.Common;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public class AccountService : IAccountService
    {
        //private readonly PRN211DemoADOContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(PRN211DemoADOContext context, IValidator<AccountModel> validator,
            IAccountRepository accountRepository, IMapper mapper)
        {
            //_context = context;
            _accountRepository = accountRepository;
            _mapper = mapper;
            //_validator = validator;
        }

        public async Task<bool> CreateAsync(AccountModel account)
        {
            //var validationResult = await _validator.ValidateAsync(account);
            //if (!validationResult.IsValid)
            //{
            //var validationException = new ValidationException(validationResult.Errors);
            //return new Result<bool>(validationException);
            //}

            var accountEntity = _mapper.Map<Account>(account);
            return await _accountRepository.CreateAsync(accountEntity);
        }

        public async Task<AccountModel> GetByEmailAsync(string email)
        {
            return await _accountRepository.GetByEmailAsync(email);
        }

        public async Task<AccountModel> GetByUserNameAsync(string username)
        {
            return await _accountRepository.GetByUsernameAsync(username);
        }

        public async Task<bool> Login(string username, string password)
        {
            return await _accountRepository.Login(username, password);
        }
    }
}
