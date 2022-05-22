using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class PlansController : BaseApiController
    {
        private readonly ApiDbContext _context;

        public PlansController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Plans>>> GetPlans()
        {
         //   var persId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(o => o.Id).FirstOrDefaultAsync();
            var plan = await _context.Plans
                //.Include(p => p.PlanDetails)
                //  .ThenInclude(p => p.Excercise)
                // .ThenInclude(t => t.ExcerciseType)
                //.Where(o => o.PersonId == persId)
                .OrderByDescending(d => d.UpdatedDate)
                .ToListAsync();

            if (plan == null) return Ok("Brak planów"); //??
            return Ok(plan);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("MyPlan")]
        public async Task<ActionResult<List<Plans>>> GetMyPlan()
        {
            var persId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(o => o.Id).FirstOrDefaultAsync();
            var plan = await _context.Plans
                .Include(p => p.Person)
                .Include(p => p.PlanDetails)
                .ThenInclude(p => p.Excercise)
                .ThenInclude(t => t.ExcerciseType)
                //.Where(o => o.PersonId == persId)
                .OrderByDescending(d => d.UpdatedDate)
                .FirstOrDefaultAsync(o => o.PersonId == persId);

            if (plan == null) return Ok("Brak planów"); //??
            return Ok(plan);
        }

        [Authorize(Roles = "Trainer")]
        [HttpGet("ClientLast")]
        public async Task<ActionResult<List<Plans>>> GetPlanClientLast(int personId)
        {
            var sizes = await _context.Plans
                .Include(p => p.Person)
                .Include(p => p.PlanDetails)
                .ThenInclude(p => p.Excercise)
                .ThenInclude(t => t.ExcerciseType)
                .OrderByDescending(o => o.UpdatedDate)
                .FirstOrDefaultAsync();
            if (sizes == null) return Ok("Brak planów"); //??
            return Ok(sizes);
        }

        [HttpGet("{id}", Name = "GetPlan")]
        public async Task<ActionResult<Plans>> GetPlan(int id)
        {
            var plan = await _context.Plans.Where(d => d.Id.Equals(id)).Include(et => et.PlanDetails).FirstOrDefaultAsync();

            if (plan == null) return NotFound();

            return Ok(plan);
        }

        [Authorize(Roles = "Trainer")]
        [HttpPost]
        public async Task<ActionResult<Plans>> AddPlan(int persId)
        {
            var plan = await RetrievePlan(persId);
            if (plan == null || !plan.UpdatedDate.Equals(DateTime.Now.Date))
            {

                var planNew = await CreatePlanAsync(persId);

                var result = await _context.SaveChangesAsync() > 0;
                if (result) return CreatedAtRoute("GetPlan", new { planNew.Id }, planNew);
            }
            return BadRequest(new ProblemDetails { Title = "Dzisiaj już istnieje dodany Plan" });
        }

        [Authorize(Roles = "Trainer")]
        [HttpGet("PlansHistory/{personId}")]
        public async Task<ActionResult<List<Plans>>> GetPlansClient(int personId)
        {
            var plan = await _context.Plans
                .Include(p => p.Person)
                .Include(p => p.PlanDetails)
                .ThenInclude(p => p.Excercise)
                .ThenInclude(t => t.ExcerciseType)
                .Where(o => o.PersonId == personId).ToListAsync();
            if (plan == null) return NotFound();
            return Ok(plan);
        }


        private async Task<Plans> RetrievePlan(int persId)
        {
           // var persId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(o => o.Id).FirstOrDefaultAsync();
            var plan = await _context.Plans
                .Include(p => p.Person)
                .Include(p => p.PlanDetails)
                .ThenInclude(p => p.Excercise)
                .ThenInclude(t => t.ExcerciseType)
                .OrderBy(d => d.UpdatedDate)
                .LastOrDefaultAsync(x => x.PersonId == persId);
            return plan;
        }

        private async Task<Plans> CreatePlanAsync(int persId)
        {
           // var persId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(o => o.Id).FirstOrDefaultAsync();
            var plan = new Plans { PersonId = persId };
            _context.Plans.Add(plan);
            return plan;
        }
    }

}
