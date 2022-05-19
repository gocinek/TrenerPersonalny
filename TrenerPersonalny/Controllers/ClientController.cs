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

namespace TrenerPersonalny.Controllers
{
    public class ClientController : BaseApiController
    {
        private readonly ApiDbContext _context;

        public ClientController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("forTrainer")]
        public async Task<ActionResult<List<OrderDto>>> GetClients()
        {
            var persId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(o => o.Id).FirstOrDefaultAsync();

            var test = await _context.Orders
                .Where(o => o.TrainerOrderedName == User.Identity.Name)
                .ToListAsync();

           return Ok(test);
        }
    }
}
