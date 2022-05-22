using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Data;
using TrenerPersonalny.Models;
using TrenerPersonalny.Extensions;

namespace TrenerPersonalny.Controllers
{
    
    public class TrainerController : BaseApiController
    {
        private readonly ApiDbContext _context;
        public TrainerController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Trainers>>> GetTrainers()
        {
            var trainers = await _context.Trainers
                .Include(tr => tr.Person)
                .ToListAsync();

            return Ok(trainers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trainers>> GetTrainer(int id)
        {
            var trainer = await _context.Trainers.Where(i => i.Id.Equals(id)).Include(tr => tr.Person).FirstOrDefaultAsync();

            if (trainer == null) return NotFound();
            
            return Ok(trainer);
        }

       
    }
}