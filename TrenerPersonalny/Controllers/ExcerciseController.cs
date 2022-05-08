using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Data;
using TrenerPersonalny.Models;
using TrenerPersonalny.Services;

namespace TrenerPersonalny.Controllers
{
    
    public class ExcerciseController : BaseApiController
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;

        public ExcerciseController(ApiDbContext context, IMapper mapper, ImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Excercises>>> GetExcercises()
        {
            var excercises = await _context.Excercises
                .Include(et => et.ExcerciseType)
                .ToListAsync();
            return Ok(excercises);
        }

        [HttpGet("{id}", Name = "GetExcercise")]
        public async Task<ActionResult<Excercises>> GetExcercise(int id)
        {
            var excercise = await _context.Excercises.Where(d => d.Id.Equals(id)).Include(et => et.ExcerciseType).FirstOrDefaultAsync();

            if (excercise == null) return NotFound();

            return Ok(excercise);
        }

        [HttpGet("type/{type}")]
        public async Task<ActionResult<Excercises>> GetExcerciseType(string type)
        {
            var excercise = await _context.Excercises.Where(p => p.ExcerciseType.Type.Equals(type)).Include(et => et.ExcerciseType).ToListAsync();
            if (excercise == null) return NotFound();

            return Ok(excercise);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Excercises>> CreateExcercise([FromForm] CreateExcerciseDTO excerciseDto)
        {

            var excercise = _mapper.Map<Excercises>(excerciseDto);

            if (excerciseDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(excerciseDto.File);

                if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                excercise.PictureUrl = imageResult.SecureUrl.ToString();
                excercise.PublicId = imageResult.PublicId;
            }


            _context.Excercises.Add(excercise);
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetExcercise", new { Id = excercise.Id }, excercise);

            return BadRequest(new ProblemDetails { Title = "Problem creating new excercise" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<Excercises>> UpdatExcercise([FromForm] UpdateExcerciseDTO excerciseDto)
        {
            var excercise = await _context.Excercises.FindAsync(excerciseDto.Id);
            if (excercise == null) return NotFound();

            _mapper.Map(excerciseDto, excercise);

            if (excerciseDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(excerciseDto.File);

                if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                if (!string.IsNullOrEmpty(excercise.PublicId))
                    await _imageService.DeleteImageAsync(excercise.PublicId);

                excercise.PictureUrl = imageResult.SecureUrl.ToString();
                excercise.PublicId = imageResult.PublicId;
            }


            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok(excercise);

            return BadRequest(new ProblemDetails { Title = "Problem updating excercise" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var excercise = await _context.Excercises.FindAsync(id);

            if (excercise == null) return NotFound();

            if (!string.IsNullOrEmpty(excercise.PublicId))
                await _imageService.DeleteImageAsync(excercise.PublicId);

            _context.Excercises.Remove(excercise);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting excercise" });
        }

    }
}