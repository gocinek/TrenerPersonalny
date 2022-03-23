using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Data;

namespace TrenerPersonalny.Controllers
{
    
    public class UserRolesController : BaseApiController
    {
        private readonly ApiDbContext _context;

        public UserRolesController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles() 
        {
            return Ok(await _context.UserRoles.ToListAsync());
        }
    }
}
