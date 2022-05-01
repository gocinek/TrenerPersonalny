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
    public class ExcerciseTypesController : BaseApiController
    {
        private readonly ApiDbContext _context;
        public ExcerciseTypesController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExcerciseType>>> GetExcerciseTypes()
        {
            return Ok(await _context.ExcerciseType.ToListAsync());
        }
    }
}
