using FirstWebAPI.Entities;
using FirstWebAPI.Heplers;
using FirstWebAPI.Payload.Request;
using FirstWebAPI.Payload.Response;
using FirstWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppSettings _appsettings;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository
            , IOptionsMonitor<AppSettings> monitor)
        {
            _userRepository = userRepository;
            _appsettings = monitor.CurrentValue;
        }

        [HttpPost("Login")]
        public IActionResult Validate(LoginRequest loginRequest)
        {
            var user = _userRepository.Login(
                            loginRequest.UserName,
                            loginRequest.Password
                        );

            if (user == null)
            {
                return Unauthorized(new BaseResponse {
                    StatusCode = 401,
                    Message = "Unauthorized"
                });
            }

            JwtHelper jwtHelper = new JwtHelper(_appsettings);
            var token = jwtHelper.GenerateToken(new UserEntity { 
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
            });

            return Ok(new BaseResponse { 
                StatusCode = 200,
                Message = "Login Sucess",
                Data = token
            });
        }
    }
}
