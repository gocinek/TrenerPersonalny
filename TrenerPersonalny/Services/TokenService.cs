using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrenerPersonalny.Models;

namespace TrenerPersonalny.Services
{
    public class TokenService
    {
        private readonly UserManager<Client> _userManager;
        private readonly IConfiguration _config;

        public TokenService(UserManager<Client> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<string> GenerateToken(Client client)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, client.Email),
                new Claim(ClaimTypes.Name, client.UserName)
            };

            var roles = await _userManager.GetRolesAsync(client);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JWTSettings:TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenOptions = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
