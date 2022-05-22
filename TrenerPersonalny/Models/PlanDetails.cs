using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models
{
    public class PlanDetails
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [ForeignKey("ExcerciseId")]
        public Excercises Excercise { get; set; }
        [Required]
        public int ExcerciseId { get; set; }
        [Range(0, 200, ErrorMessage = "Value for Repeats must be between {0} and {200}.")]
        [Required]
        public int Repeats { get; set; }
        [Required]
        [Range(0, 7, ErrorMessage = "Value for ManyInWeek must be between {0} and {7}.")]
        public int ManyInWeek { get; set; }
    }
}
