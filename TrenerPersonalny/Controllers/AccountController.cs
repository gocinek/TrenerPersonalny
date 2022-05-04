using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Models;
using TrenerPersonalny.Models.DTOs;
using TrenerPersonalny.Models.DTOs.Requests;
using TrenerPersonalny.Models.DTOs.Responses;
using TrenerPersonalny.Services;

namespace TrenerPersonalny.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<Client> _userManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<Client> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserLoginRequest loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user)
            };                
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegistrationDto registrationDto, Boolean tr)
        {
            var user = new Client();
            if (tr)
            {
                user = new Client
                {
                    Email = registrationDto.Email,
                    UserName = registrationDto.Username,
                    Registered = DateTime.Today.ToString("d"),
                    Person = new Person
                    {
                        LastName = registrationDto.Person.LastName,
                        FirstName = registrationDto.Person.FirstName,
                        Trainers = new Trainers
                        {
                            Description = "Opis nie został jeszcze dodany",
                            Price = 0,
                            Rating = 0
                        }
                    }
                };
            } else
            {
                user = new Client
                {
                    Email = registrationDto.Email,
                    UserName = registrationDto.Username,
                    Registered = DateTime.Today.ToString("d"),
                    Person = new Person
                    {
                        LastName = registrationDto.Person.LastName,
                        FirstName = registrationDto.Person.FirstName
                    }
                };
            }

                var result = await _userManager.CreateAsync(user, registrationDto.Password);

            
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }
            var rola = "Client";
            if (tr)
            {
                rola = "Trainer";
            }
            await _userManager.AddToRoleAsync(user, rola);

            return StatusCode(201); //Succes code
        }


        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user)
            };
        }
    }
}
