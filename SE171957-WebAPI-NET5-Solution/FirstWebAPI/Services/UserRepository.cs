using FirstWebAPI.Heplers;
using FirstWebAPI.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextHelper _context;

        public UserRepository(DbContextHelper context)
        {
            _context = context;
        }

        public UserResponse Login(string username, string password)
        {
            var user = _context.Users
                               .Where(x => x.UserName == username 
                                        && x.Password == password).FirstOrDefault();
            if(user != null)
            {
                return new UserResponse
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Email = user.Email,
                };
            }

            return null;
        }
    }
}
