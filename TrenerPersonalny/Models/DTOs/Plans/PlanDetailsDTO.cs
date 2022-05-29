using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Models;

namespace TrenerPersonalny.Models.DTOs.Plans
{
    public class PlanDetailsDTO
    {
       // public Excercises Excercise { get; set; }
        [Required]
        public int ExcerciseId { get; set; }
        [Required]
        public int Repeats { get; set; }
        [Required]
        public int ManyInWeek { get; set; }
       // [Required]
       // public int PersonId { get; set; }
    }
}
