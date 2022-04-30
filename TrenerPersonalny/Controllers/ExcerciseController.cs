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
    
    public class ExcerciseController : BaseApiController
    {
        private readonly ApiDbContext _context;
        public ExcerciseController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Excercises>>> GetExcercises()
        {
            var excercises = await _context.Excercises
                .Include(et => et.ExcerciseType)
                .ToListAsync();
            return Ok(excercises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Excercises>> GetExcercise(int id)
        {
            var excercise = await _context.Excercises.Where(d => d.Id.Equals(id)).Include(et => et.ExcerciseType).FirstOrDefaultAsync();

            if (excercise == null) return NotFound();

            return Ok(excercise);
        }

        [HttpGet("type/{type}")]
        public async Task<ActionResult<Excercises>> GetExcerciseType(string type)
        {
            var excercise = await _context.Excercises.Where(p => p.ExcerciseType.Type.Equals(type)).Include(et => et.ExcerciseType).ToListAsync();
            if (excercise == null) return NotFound();

            return Ok(excercise);
        }
    }
}