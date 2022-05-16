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
                .ToListAsync();
            if (sizes == null) return NotFound(); //??
            return sizes;
        } 

        [HttpGet("last")]
        public async Task<ActionResult<Sizes>> GetLast()
        {
            var sizes = await _context.Sizes
               .Where(s => s.Person.Client.UserName == User.Identity.Name)
               .Include( o => o.SizeDetails)
               .ThenInclude( o => o.ExcerciseType)
               .OrderBy(o => o.UpdateDate)
               .LastOrDefaultAsync();
           // Console.WriteLine(sizes.UpdateDate);
            return sizes;
        }

        [HttpGet("{id}", Name = "GetSize")]
        public async Task<ActionResult<SizesDTO>> GetSize(int id)
        {
            var size = await _context.Sizes.Where(d => d.Id.Equals(id)).Include(et => et.SizeDetails).FirstOrDefaultAsync();

            if (size == null) return NotFound();

            return Ok(size);
        }

     /*   [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<ActionResult<SizesDTO>> CreateSizeForm([FromForm] CreateSizeDTO sizesDto)
        {
            var persId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(o => o.Id).FirstOrDefaultAsync();
            //   sizesDto.PersonId = persId;

            //var last = GetLast();
            var last = RetrieveSizes();
            var size = _mapper.Map<Sizes>(sizesDto);
           // Console.WriteLine(last.Result.Value.UpdateDate);
          //  Console.WriteLine(DateTime.Now.Date);
            if (last.Result.UpdateDate.Equals(DateTime.Now.Date))
            {
                return BadRequest(new ProblemDetails { Title = "Please use HttpPut to this purpose" });
            }

            size.PersonId = persId;

            _context.Sizes.Add(size);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetSize", new { Id = size.Id }, size);

            return BadRequest(new ProblemDetails { Title = "Problem creating new size" });
        }*/

        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<ActionResult<SizesDTO>> AddSizeToSizes([FromForm] SizeDetailsDTO sizeDetailsDto)
        {           
            var size = await RetrieveSizes(); 
            if (size == null || !size.UpdateDate.Equals(DateTime.Now.Date))
            {
                //Console.WriteLine(size.Id);
                size = await CreateSizeAsync();
                _context.Sizes.Add(size);
            } 
            foreach (var te in size.SizeDetails)
            {
                if (te.ExcerciseTypeId == sizeDetailsDto.ExcerciseTypeId)
                {
                    return BadRequest(new ProblemDetails { Title = "ExcerciseTypeId: " + sizeDetailsDto.ExcerciseTypeId + " already exists today. Please use HttpPut to this purpose" });
                }
            }
            
            size.AddDetail(sizeDetailsDto.ExcerciseTypeId, sizeDetailsDto.SizeCm);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetSize", new { Id = size.Id }, size);

            return BadRequest(new ProblemDetails { Title = "Problem saving details to size" });
        }

        [Authorize(Roles = "Client")]
        [HttpDelete]
        public async Task<ActionResult> RemoveSizeDetail(int detailId, int sizesId)
        {
            var size = await RetrieveSizes();

            if (size == null) return NotFound();

            size.RemoveDetail(detailId, sizesId);
            
            if(size == null)
            {
                 _context.Sizes.Remove(size);
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem removing size detail from the SizesDetails" });
        }

        [Authorize(Roles = "Client")]
        [HttpDelete("all")]
        public async Task<ActionResult> RemoveSize(int sizesId)
        {
            var size = await RetrieveSizes();

            if (size == null) return NotFound();

            //size.RemoveDetail(sizesId);

            _context.Sizes.Remove(size);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem removing size" });
        }


        private async Task<Sizes> RetrieveSizes()
        {          
            var persId = await _context.Person.Where(i => i.Client.UserName == User.Identity.Name).Select(o => o.Id).FirstOrDefaultAsync();
            return await _context.Sizes
                .Include(i => i.SizeDetails)    
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
