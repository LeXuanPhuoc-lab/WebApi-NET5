using FPTManager.Entities;
using FPTManager.Models;
using System.Threading.Tasks;

namespace FPTManager.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> CreateAsync(Account account);
        Task<bool> Login(string username, string password);
        Task<AccountModel> GetByEmailAsync(string email); 
        Task<AccountModel> GetByUsernameAsync(string email);
    }
}
