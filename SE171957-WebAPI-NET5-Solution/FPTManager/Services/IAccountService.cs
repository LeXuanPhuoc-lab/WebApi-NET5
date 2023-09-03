using FPTManager.Entities;
using FPTManager.Models;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public interface IAccountService
    {
        Task<bool> CreateAsync(AccountModel account);
        Task<bool> Login(string username, string password);
        Task<AccountModel> GetByUserNameAsync(string username);
        Task<AccountModel> GetByEmailAsync(string email);
    }
}
