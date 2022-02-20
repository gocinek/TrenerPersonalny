using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrenerPersonalny.Configuration;
using TrenerPersonalny.Models;
using TrenerPersonalny.Models.DTOs.Reponses;
using TrenerPersonalny.Models.DTOs.Requests;

namespace TrenerPersonalny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<Client> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthManagementController(UserManager<Client> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if(existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                    {
                        "Email jest już używany"
                    },
                        Success = false
                    });
                }

                var salt = HashPass.salt();
                var newUser = new Client() { Email = user.Email, UserName = user.Email.Substring(0, user.Email.IndexOf("@")), Password = HashPass.hashPass(user.Password, salt), PasswordSalt = salt, rolesId = user.rolesId, Registered = DateTime.Today.ToString("d")  }; //, 
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);

                    return Ok(new RegistrationResponse(){
                        Success = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors =  isCreated.Errors.Select(x => x.Description)
                            .ToList(),
                            Success = false
                    });
                }
            }

            return BadRequest(new RegistrationResponse() { 
                Errors = new List<string>()
                {
                    "error"
                },
                    Success = false
            });
        } 

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if(existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Podany użytkownik nie istnieje"
                        },
                        Success = false
                    });
                }
            
                var verifyPass = HashPass.VerifyPassword(user.Password, existingUser.Password, existingUser.PasswordSalt);
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, HashPass.hashPass(user.Password, existingUser.PasswordSalt));
                if (!verifyPass)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Niepoprawne dane logowania"
                        },
                        Success = false
                    });
                }

                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(new RegistrationResponse()
                {
                    Success = true,
                    Token = jwtToken
                });

            } 

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                {
                    "error"
                },
                Success = false
            });
        }

        private string GenerateJwtToken(Client user)
        {
            var jwtokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtokenHandler.WriteToken(token);

            return jwtToken;
        }
                
    }
}
