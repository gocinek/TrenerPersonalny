using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Data;
using TrenerPersonalny.Models;
using TrenerPersonalny.Models.DTOs.Plans;

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
                .Include(p => p.PlanDetails)
                .ThenInclude(p => p.Excercise)
                .ThenInclude(t => t.ExcerciseType)
                 //.Where(o => o.PersonId == persId)
                 .Include(p => p.Person)
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
            var plan = await _context.Plans.Where(d => d.Id.Equals(id)).Include(et => et.PlanDetails).ThenInclude(et => et.Excercise).ThenInclude(t=>t.ExcerciseType).FirstOrDefaultAsync();

            if (plan == null) return NotFound();

            return Ok(plan);
        }

        [Authorize(Roles = "Trainer")]
        [HttpPost]
        public async Task<ActionResult<Plans>> AddPlan(int personId)
        {
            var plan = await RetrievePlan(personId);
            if (plan == null || !plan.UpdatedDate.Equals(DateTime.Now.Date))
            {

                var planNew = await CreatePlanAsync(personId);

                var result = await _context.SaveChangesAsync() > 0;
                if (result) return CreatedAtRoute("GetPlan", new { Id =  planNew.Id }, planNew);
            }
            return BadRequest(new ProblemDetails { Title = "Dzisiaj już istnieje dodany Plan" });
        }

        [Authorize(Roles = "Trainer")]
        [HttpPut("AddPlanDet")]
        public async Task<ActionResult<Plans>> AddPlanDet([FromForm] PlanDetailsDTO planDetailsDto, int personId)
        {
          //  var personId = planDetailsDto.PersonId;
            var plan = await RetrievePlan(personId);

            if (plan == null || !plan.UpdatedDate.Equals(DateTime.Now.Date))
            {
                var planNew = await CreatePlanAsync(personId);
            }
            plan.AddDetail(planDetailsDto.ExcerciseId, planDetailsDto.Repeats, planDetailsDto.ManyInWeek);
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetPlan", new { Id = plan.Id }, plan);
            return BadRequest(new ProblemDetails { Title = "Ćwiczenie już istnieje, dodaj inny rodzaj" });
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
                .OrderByDescending(o => o.UpdatedDate)
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
            var trainerId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(i => i.TrainerId).FirstOrDefaultAsync();
            var plan = new Plans {

                PersonId = persId,
                TrainerId = (int)trainerId            
            };
            _context.Plans.Add(plan);
            return plan;
        }
    }

}
