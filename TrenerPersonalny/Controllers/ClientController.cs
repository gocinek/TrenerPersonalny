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

namespace TrenerPersonalny.Controllers
{
    [Authorize]
    public class ClientController : BaseApiController
    {
        private readonly ApiDbContext _context;

        public ClientController(ApiDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetUsers()
        {
            var users = await _context.Person
               // .Include(o => o.Client.Email)
                .Include(o => o.Client)                
                .ToListAsync();
            if (users == null) return Ok("Brak użytkowników w serwisie");
            return Ok(users);
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
