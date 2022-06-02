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
using Microsoft.AspNetCore.Authorization;
using TrenerPersonalny.Models.DTOs;

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

        [Authorize(Roles = "Trainer")]
        [HttpGet("TrainerProfile")]
        public async Task<ActionResult<List<Trainers>>> GetTrainerProfile()
        {
            var trainer = await _context.Trainers
                .Where(o => o.Person.Client.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();
            if (trainer == null) return NotFound();
            return Ok(trainer);
        }

        [Authorize(Roles = "Trainer")]
        [HttpPut("TrainerProfileUpdate")]
        public async Task<ActionResult<Trainers>> UpdateTrainerProfile([FromForm] UpdateTrainerDTO trainerDTO)
        {
            var trainer = await _context.Trainers
                 .Where(o => o.Person.Client.UserName == User.Identity.Name)
                 .FirstOrDefaultAsync();
            if (trainer == null) return NotFound();
            if (trainerDTO.Description != null) trainer.Description = trainerDTO.Description;
            trainer.Description = trainerDTO.Description;

            if (trainerDTO.Price != null)
            {
                if (trainerDTO.Price < 0)
                {
                    trainerDTO.Price = 0;
                }
                trainer.Price = trainerDTO.Price;
            }   

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok(trainer);

            return BadRequest(new ProblemDetails { Title = "Problem updating trainer profile" });
        }
    }
}