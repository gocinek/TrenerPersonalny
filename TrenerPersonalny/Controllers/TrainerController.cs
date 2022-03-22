using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Data;
using TrenerPersonalny.Models;

namespace TrenerPersonalny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public TrainerController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Trainers>>> GetTrainers()
        {
            var excercises = await _context.Trainers
                .Include(tr => tr.person)
                .ToListAsync();
            return Ok(excercises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trainers>> GetTrainer(int id)
        {
            return await _context.Trainers.FindAsync(id);
        }


    }
}