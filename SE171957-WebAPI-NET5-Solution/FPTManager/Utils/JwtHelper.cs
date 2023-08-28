using FPTManager.Entities;
using FPTManager.Models;
using FPTManager.Payloads.Request;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FPTManager.Utils
{
    public class JwtHelper
    {

        private readonly AppSettings _appSettings;

        public JwtHelper(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public string GenerateToken(Account account) 
        {
            //1. secret key bytes
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            //2. token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            //3. token description
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("UserName", account.Username),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256)
            };
            //4. generate token 
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }





        //private readonly AppSettings _appSettings;

        //public JwtHelper(AppSettings appSettings)
        //{
        //    _appSettings = appSettings;
        //}

        //public String GenerateToken(LoginRequest loginModel) {
        //    // Secret key bytes arr
        //    var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

        //    // Token Handler
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    // Token Description
        //    var tokenDescription = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] { 
        //            new Claim(ClaimTypes.Email, loginModel.Email),
        //            new Claim(ClaimTypes.Name, loginModel.FirstName + loginModel.MidName + loginModel.LastName),
        //            new Claim("Username", loginModel.Username),
        //            new Claim("TokenId", Guid.NewGuid().ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(1),
        //        SigningCredentials = new SigningCredentials(
        //            new SymmetricSecurityKey(secretKeyBytes),
        //            SecurityAlgorithms.HmacSha256
        //            )
        //    };
        //    // Create Token
        //    var token = tokenHandler.CreateToken(tokenDescription);

        //    return tokenHandler.WriteToken(token);
        //}

    }
}
