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

        [HttpGet("MyPlan")]
        public async Task<ActionResult<List<Plans>>> GetMyPlan()
        {
            var persId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(o => o.Id).FirstOrDefaultAsync();
            var plan = await _context.Plans
                .Include(p => p.PlanDetails)
                .ThenInclude(p => p.Excercise)
                .ThenInclude(t => t.ExcerciseType)
                .Where(o => o.PersonId == persId)
                .OrderByDescending(d => d.UpdatedDate)
                .FirstOrDefaultAsync();

            if (plan == null) return Ok("Brak planów"); //??
            return Ok(plan);
        }


        [HttpGet("PlansHistory/{personId}")]
        public async Task<ActionResult<List<Plans>>> GetPlansClient(int personId)
        {
            var plan = await _context.Plans.Include(i => i.PlanDetails).Where(o => o.PersonId == personId).ToListAsync();
            if (plan == null) return NotFound();
            return Ok(plan);
        }


    }

}
