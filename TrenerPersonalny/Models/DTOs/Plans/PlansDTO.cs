using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models.DTOs.Plans
{
    public class PlansDTO
    {
      //  public Trainers Trainers { get; set; }
        [Required]
        public int TrainerId { get; set; }
      //  public Person Person { get; set; }
        [Required]
        public int PersonId { get; set; }

        public List<PlanDetails> PlanDetails { get; set; }
        public DateTime UpdatedDate;
    }
}
