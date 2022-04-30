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
        public async Task<ActionResult> Register(UserRegistrationDto registrationDto)
        {
            var user = new Client
            {
                UserName = registrationDto.Username,
                Email = registrationDto.Email,
                Registered = DateTime.Today.ToString("d"),
                Person = new Person
                {
                    LastName = registrationDto.Person.LastName,
                    FirstName = registrationDto.Person.FirstName,
                    Trainers = new Trainers
                    {
                        Description = registrationDto.Person.Trainers.Description,
                        Price = registrationDto.Person.Trainers.Price,
                    }
                }
            };
            var result = await _userManager.CreateAsync(user, registrationDto.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, "Trainer");

            return StatusCode(201); //Succes code
        }

        [Authorize]
        [HttpGet("currenUser")]
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
