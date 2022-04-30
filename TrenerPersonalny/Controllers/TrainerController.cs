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
        public async Task<ActionResult<List<Trainers>>> GetTrainers(string orderBy,
            string searchTerm, int price = 1000000, int rating = 0)
        {
            var trainers = _context.Trainers
                .Include(tr => tr.Person)
                .Sort(orderBy)
                .Search(searchTerm)
                .Filter(price, rating)
                .AsQueryable();

            return await trainers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trainers>> GetTrainer(int id)
        {
            var trainer = await _context.Trainers.Where(i => i.Id.Equals(id)).Include(tr => tr.Person).FirstOrDefaultAsync();

            if (trainer == null) return NotFound();
            
            return Ok(trainer);
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var prices = await _context.Trainers.Select(t => t.Price).Distinct().ToListAsync();
            var ratings = await _context.Trainers.Select(t => t.Rating).Distinct().ToListAsync();
            var genders = await _context.Trainers.Select(t => t.Person.Gender).Distinct().ToListAsync();
            var languages = await _context.Trainers.Select(t => t.Person.Language).Distinct().ToListAsync();

            return Ok(new { prices, ratings, genders, languages });

        }
    }
}