using FirstWebAPI.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace FirstWebAPI.Heplers
{
    public class JwtHelper
    {

        private readonly AppSettings _appSettings;

        public JwtHelper(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public string GenerateToken(UserEntity userEntity)
        {
            //1. Get secretkey bytes
            var secretkeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            //2. jwt token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            //3. token descriptions/details
            var tokenDescription = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(ClaimTypes.Name, userEntity.FullName),
                    new Claim("Username", userEntity.UserName),
                    new Claim("Id", userEntity.UserId.ToString()),

                    // Role

                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(secretkeyBytes),
                        SecurityAlgorithms.HmacSha256Signature
                    )
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token); // Write Token
        }
    }
}
