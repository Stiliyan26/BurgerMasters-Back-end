using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(ExportUserDto userInfo, string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = GetClaims(userInfo, userId);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(ExportUserDto userInfo, string userId)
        {
            return GenerateToken(userInfo, userId);
        }


        public Claim[] GetClaims(ExportUserDto userInfo, string userId)
        {
            return new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Name, userInfo.Username),
                new Claim(ClaimTypes.DateOfBirth, userInfo.Birthday.ToString()),
                new Claim(ClaimTypes.Role, userInfo.Role ?? string.Empty)
            };
        }


        /*public ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var calimsIdentity = new ClaimsIdentity(jwtToken.Claims, "JWT");
            var claimsPrincipal = new ClaimsPrincipal(calimsIdentity);

            return claimsPrincipal;
        }*/
    }
}
