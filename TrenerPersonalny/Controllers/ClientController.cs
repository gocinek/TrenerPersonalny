using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Data;
using Microsoft.EntityFrameworkCore;
using TrenerPersonalny.Models;
using Microsoft.AspNetCore.Authorization;
using TrenerPersonalny.Models.DTOs.Users;
using AutoMapper;
using TrenerPersonalny.Services;
using TrenerPersonalny.Models.DTOs.Profile;

namespace TrenerPersonalny.Controllers
{
    [Authorize]
    public class ClientController : BaseApiController
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;

        public ClientController(ApiDbContext context, IMapper mapper, ImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _context.Client
                .Include(o => o.Person)
                .ThenInclude(t => t.Trainers)
                .ToListAsync();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> GetUsers(int id)
        {
            var user = await _context.Client
                .Include(o => o.Person)
                .ThenInclude(t => t.Trainers)
                .Where(o => o.Id == id)
                .ToListAsync();
            if (user == null) return Ok("Brak użytkownika serwisie");
            return Ok(user);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<User>> UpdatePerson([FromForm] UpdateUserDTO userDto)
        {
            var user = await _context.Client
                .Include(p => p.Person)
                .Where(o => o.PersonId.Equals(userDto.Id))
                .FirstOrDefaultAsync();
            if (user == null) return NotFound();
            
            _mapper.Map(userDto, user);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok(user);

            return BadRequest(new ProblemDetails { Title = "Problem updating user" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.Include(p => p.Person).Where(i =>i.Id == id).FirstOrDefaultAsync();

            if (user == null) return NotFound();

            if (!string.IsNullOrEmpty(user.Person.PublicId))
                await _imageService.DeleteImageAsync(user.Person.PublicId);

            _context.Users.Remove(user);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting user: " + user.Person.LastName });
        }

        [Authorize(Roles = "Trainer")]
        [HttpGet("forTrainer")]
        public async Task<ActionResult<List<Person>>> GetClients()
        {
            var persId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(o => o.Id).FirstOrDefaultAsync();

            var test = await _context.Orders
                .Where(o => o.TrainerOrderedName == User.Identity.Name)
                .Where(o => o.Expired >= DateTime.Now.Date)
                .Select(o => o.BuyerId)
                .Distinct()
                .ToListAsync();

            List<Person> person = new List<Person>();
            foreach(string t in test)
            {
                var ti = await _context.Person.Where(o => o.Client.UserName == t).FirstOrDefaultAsync();  //.Select(o => string.Concat(o.LastName, " ", o.FirstName))
                person.Add(ti);  
            }
           return Ok(person);
        }

        [Authorize]
        [HttpGet("MyProfile")]
        public async Task<ActionResult<List<Person>>> GetProfile()
        {
            var user = await _context.Person
                .Where(o => o.Client.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();
            if (user == null) return NotFound();
            return Ok(user);
        }

        [Authorize]
        [HttpPut("MyProfileUpdate")]
        public async Task<ActionResult<List<Person>>> UpdateProfile([FromForm] UpdatePersonDTO updatePersonTdo)
        {
            var user = await _context.Person
                .Where(o => o.Client.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();
            if (user == null) return NotFound();
            if (updatePersonTdo.FirstName != null) user.FirstName = updatePersonTdo.FirstName;
            if (updatePersonTdo.LastName != null) user.LastName = updatePersonTdo.LastName;
           
            if (updatePersonTdo.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(updatePersonTdo.File);

                if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                if (!string.IsNullOrEmpty(user.PublicId))
                    await _imageService.DeleteImageAsync(user.PublicId);

                user.ProfileImg = imageResult.SecureUrl.ToString();
                user.PublicId = imageResult.PublicId;
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok(user);

            return BadRequest(new ProblemDetails { Title = "Problem updating profile" });
        }

    }
}
