using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Configuration;
using TrenerPersonalny.Data;
using TrenerPersonalny.Models;

namespace TrenerPersonalny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ClientController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _context.Client.ToListAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(string id)
        {
            var client = await _context.Client.FirstOrDefaultAsync(x => x.Id == id);

            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

     /*   [HttpPost]
        public async Task<IActionResult> CreateClient(Client data)
        {
            if (ModelState.IsValid)
            {
                await _apiDbcontext.Client.AddAsync(data);
                await _apiDbcontext.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateClient), new { data.Id }, data);
            }
            return new JsonResult("Coś poszło nie tak. Nie można dodać") { StatusCode = 500 };
        }*/

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(string id, Client data)
        {
            if (id != data.Id)
            {
                return BadRequest();
            }

            var existClient = await _context.Client.FirstOrDefaultAsync(x => x.Id == id);

            if (existClient == null)
            {
                return NotFound();
            }
            existClient.LastName = data.LastName;
            existClient.FirstName = data.FirstName;
            existClient.Email = data.Email;
            existClient.Gender = data.Gender;
            existClient.Language = data.Language;
            existClient.Nationality = data.Nationality;
            var saltPass = HashPass.salt();
            existClient.Password = HashPass.hashPass(data.Password, saltPass);
            existClient.PasswordSalt = saltPass;
            existClient.PhoneNumber = data.PhoneNumber;
            existClient.ProfileImg = data.ProfileImg;
            existClient.Registered = data.Registered;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(string id)
        {

            var existClient = await _context.Client.FirstOrDefaultAsync(x => x.Id == id);
            if (existClient == null)
            {
                return NotFound();
            }

            _context.Client.Remove(existClient);

            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}
