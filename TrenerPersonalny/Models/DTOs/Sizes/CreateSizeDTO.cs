using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models.DTOs.Sizes
{
    public class CreateSizeDTO
    {

        //  [Required]
        //  public int PersonId { get; set; }
        [Required]
        public int SizeCm { get; set; } //cm
        [Required]
        public int ExcerciseTypeId { get; set; }
        //public List<SizeDetails> SizeDetails { get; set; }
    }
}
