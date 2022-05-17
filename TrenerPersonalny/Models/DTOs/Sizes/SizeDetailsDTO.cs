using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Models;

namespace TrenerPersonalny.Models.DTOs.Sizes
{
    public class SizeDetailsDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [Required]
        public int SizeCm { get; set; } //cm
        [Required]
        public int ExcerciseTypeId { get; set; }
        public ExcerciseType ExcerciseType { get; set; }
        //public int SizesId { get; set; }
    }
}
