using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Data;
using TrenerPersonalny.Models.Orders;
using Microsoft.EntityFrameworkCore;
using TrenerPersonalny.Models.DTOs.Orders;
using TrenerPersonalny.Extensions;
using TrenerPersonalny.Models;
using Microsoft.AspNetCore.Authorization;
using TrenerPersonalny.Models.DTOs.Users;
using AutoMapper;
using TrenerPersonalny.Services;

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
        public async Task<ActionResult<List<Person>>> GetUsers()
        {
            var users = await _context.Person
                .Include(o => o.Client)
                .Include(t => t.Trainers)
                .ToListAsync();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Person>>> GetUsers(int id)
        {
            var user = await _context.Person
                .Include(o => o.Client)
                .Include(t => t.Trainers)
                .Where(o => o.Id == id)
                .ToListAsync();
            if (user == null) return Ok("Brak użytkownika serwisie");
            return Ok(user);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<Person>> UpdatPerson([FromForm] UpdatePersonDTO personDto)
        {
            var user = await _context.Person.FindAsync(personDto.Id);
            if (user == null) return NotFound();

            _mapper.Map(personDto, user);

            if (personDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(personDto.File);

                if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                if (!string.IsNullOrEmpty(user.PublicId))
                    await _imageService.DeleteImageAsync(user.PublicId);

                user.ProfileImg = imageResult.SecureUrl.ToString();
                user.PublicId = imageResult.PublicId;
            }
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok(user);

            return BadRequest(new ProblemDetails { Title = "Problem updating user" });
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
    }
}
