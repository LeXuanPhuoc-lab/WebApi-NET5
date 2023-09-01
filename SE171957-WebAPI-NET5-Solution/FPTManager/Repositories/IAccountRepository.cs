using FPTManager.Entities;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Repositories
{
    public interface IAccountRepository
    {
        Task<Result<bool>> CreateAsync(Account account);
    }
}
