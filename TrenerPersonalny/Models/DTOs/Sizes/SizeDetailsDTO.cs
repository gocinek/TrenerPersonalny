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

        [Range(0, 200, ErrorMessage = "Value for {0} must be between {0} and {200}.")]
        [Required]
        public int SizeCm { get; set; } //cm
        [Required]
        public int ExcerciseTypeId { get; set; }
        public ExcerciseType ExcerciseType { get; set; }
        //public int SizesId { get; set; }
    }
}
