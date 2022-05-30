using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Data;
using TrenerPersonalny.Extensions;
using TrenerPersonalny.Models;
using TrenerPersonalny.Models.DTOs.Sizes;

namespace TrenerPersonalny.Controllers
{
    public class SizesController : BaseApiController
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public SizesController(ApiDbContext context, IMapper mapper)
        {
           _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SizesDTO>>> GetSizes()
        {
            var sizes = await _context.Sizes
                .MapSizesToDto()
                .OrderByDescending(o => o.UpdateDate)
                .ToListAsync();
            if (sizes == null) return NotFound(); //??
            return sizes;
        }

        [Authorize(Roles = "Client")]
        [HttpGet("MySizes")]
        public async Task<ActionResult<List<SizesDTO>>> GetMySizes()
        {
            var persId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(o => o.Id).FirstOrDefaultAsync();
            var sizes = await _context.Sizes
                .MapSizesToDto()
                .Where(o =>o.PersonId == persId)
                .OrderByDescending(d => d.UpdateDate)
                .ToListAsync();
                
            if (sizes == null) return Ok("Brak pomiarów"); //??
            return Ok(sizes);
        }

        [Authorize(Roles = "Trainer")]
        [HttpGet("ClientLast")]
        public async Task<ActionResult<List<SizesDTO>>> GetSizesClientLast(int personId)
        {
            var sizes = await _context.Sizes
                .MapSizesToDto()
                .Where(o => o.PersonId == personId)
                .OrderByDescending(o => o.UpdateDate)
                .FirstOrDefaultAsync();
            if (sizes == null) return Ok("Brak pomiarów"); //??
            return Ok(sizes);
        }

        [Authorize(Roles = "Trainer")]
        [HttpGet("SizesHistory/{personId}")]
        public async Task<ActionResult<List<SizesDTO>>> GetSizesClient(int personId)
        {
            var sizes = await _context.Sizes
                .MapSizesToDto()
                .Where(o => o.PersonId == personId)
                .OrderByDescending(o => o.UpdateDate)
                .ToListAsync();
            if (sizes == null) return Ok("Brak pomiarów"); //??
            return Ok(sizes);
        }

        [HttpGet("{id}", Name = "GetSize")]
        public async Task<ActionResult<SizesDTO>> GetSize(int id)
        {
            var size = await _context.Sizes.Where(d => d.Id.Equals(id)).Include(et => et.SizeDetails).FirstOrDefaultAsync();

            if (size == null) return NotFound();

            return Ok(size);
        }

        [HttpGet("sizeDet")]
        public async Task<ActionResult<SizeDetailsDTO>> GetSizeDet(int excerciseTypeId)
        {
            var size = await RetrieveSizes();

            if (size == null || !size.UpdateDate.Equals(DateTime.Now.Date)) return NotFound("Proszę użyć HTTPOST");

            var sizeDet = await _context.SizeDetails
                .Where(i => i.SizesId == size.Id)
                .Where(et => et.ExcerciseTypeId == excerciseTypeId)
                .FirstOrDefaultAsync();

            return Ok(sizeDet);
        }

        [Authorize(Roles = "Client")] /// chyba wurzucic
        [HttpPut]
        public async Task<ActionResult<SizesDTO>> UpdateSizeDetail([FromForm] SizeDetailsDTO sizeDetailsDto)
        {
            var size = await RetrieveSizes();
            if (size == null || !size.UpdateDate.Equals(DateTime.Now.Date)) return NotFound();

            size.UpdateDetail(sizeDetailsDto.ExcerciseTypeId, sizeDetailsDto.SizeCm);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetSize", new { Id = size.Id }, size);

            return BadRequest(new ProblemDetails { Title = "Problem saving details to size" });
        }

        [Authorize(Roles = "Client")]
        [HttpPut("updateAddCm")]
        public async Task<ActionResult<SizesDTO>> UpdateAddCm(int excerciseTypeId)
        {
            var size = await RetrieveSizes();
            if (size == null || !size.UpdateDate.Equals(DateTime.Now.Date)) return NotFound();

            size.UpdateCm(excerciseTypeId, true);

            var sizeDet = await _context.SizeDetails
                .Where(o => o.SizesId == size.Id)
                .Where(t => t.ExcerciseTypeId == excerciseTypeId)
                .FirstOrDefaultAsync();

            if (sizeDet.SizeCm > 200)
            {
                sizeDet.SizeCm = 200;
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetSize", new { Id = size.Id }, size);

            return BadRequest(new ProblemDetails { Title = "Rozmiar nie może być większy niż 200" });
        }

        [Authorize(Roles = "Client")]
        [HttpPut("updateSubCm")]
        public async Task<ActionResult<SizesDTO>> UpdateSubCm(int excerciseTypeId)
        {
            var size = await RetrieveSizes();
            if (size == null || !size.UpdateDate.Equals(DateTime.Now.Date)) return NotFound();

            size.UpdateCm(excerciseTypeId, false);

            var sizeDet = await _context.SizeDetails
                .Where(o => o.SizesId == size.Id)
                .Where(t => t.ExcerciseTypeId == excerciseTypeId)
                .FirstOrDefaultAsync();

            if (sizeDet.SizeCm < 0)
            {
                sizeDet.SizeCm = 0;
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetSize", new { Id = size.Id }, size);

            return BadRequest(new ProblemDetails { Title = "Rozmiar nie może być mniejszy niż 0" });
            }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<ActionResult<SizesDTO>> AddSize()
        {           
            var size = await RetrieveSizes(); 
            if (size == null || !size.UpdateDate.Equals(DateTime.Now.Date))
            {

                var sizeNew = await CreateSizeAsync();

                foreach(int i in _context.ExcerciseType.Select(i => i.Id).ToList())
                {
                    sizeNew.AddDetail(i, 0);
                }

                var result = await _context.SaveChangesAsync() > 0;
                if (result) return CreatedAtRoute("GetSize", new { Id = sizeNew.Id }, sizeNew);
            } 
            return BadRequest(new ProblemDetails { Title = "Dzisiaj już istnieje dodany Size" });
        }

        [Authorize(Roles = "Client")]
        [HttpDelete]
        public async Task<ActionResult> RemoveSizeDetail(int excerciseTypeId)
        {
            var size = await RetrieveSizes();

            if (size == null) return NotFound();
           
            size.RemoveDetail(excerciseTypeId);
            if (size.SizeDetails.Count == 0)
            {
              //  Console.WriteLine("jestem tutaj");
                _context.Sizes.Remove(size);
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem removing size detail from the SizesDetails" });
        }

        private async Task<Sizes> RetrieveSizes()
        {          
            var persId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(o => o.Id).FirstOrDefaultAsync();
            return await _context.Sizes
                .Include(i => i.SizeDetails)
                .ThenInclude(i => i.ExcerciseType)
                .OrderBy(d => d.UpdateDate)
                .LastOrDefaultAsync(x => x.PersonId == persId);
        }

        private async Task<Sizes> CreateSizeAsync()
        {
            var persId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(o => o.Id).FirstOrDefaultAsync();
            var size = new Sizes { PersonId = persId };
            _context.Sizes.Add(size);
            return size;
        }
        
    }
}
