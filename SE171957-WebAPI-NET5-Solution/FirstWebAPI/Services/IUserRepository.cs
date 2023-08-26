using FirstWebAPI.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Services
{
    public interface IUserRepository
    {
        UserResponse Login(string username, string password);
    }
}
