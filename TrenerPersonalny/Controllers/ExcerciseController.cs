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
                .Include(et => et.type)
                .ToListAsync();
            return Ok(excercises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Excercises>> GetExcercise(int id)
        {
            return await _context.Excercises.FindAsync(id);
        }
    }
}