using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models
{
    public class CreateExcerciseDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile File { get; set; }

        //  public string Type { get; set; }
                
        [Required]
        public int ExcerciseTypeId { get; set; }
    }
}
